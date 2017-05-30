using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.EventArguments;

namespace SimpleNeuralNetwork.AI
{
    public class NeuralNetworkTrainer
    {
        private IFeedForward _feedForward;
        private IBackPropagate _backPropagate;
        private INetworkLayers _networkLayers;

        public delegate void LearningCycleStartHandler(object sender, LearningCycleStartEventArgs e);
        public event LearningCycleStartHandler OnLearningCycleStart;
        public delegate void SampleLearnedHandler(object sender, SampleEventArgs e);
        public event SampleLearnedHandler OnSampleLearned;
        public delegate void LearningCycleCompleteHandler(object sender, LearningCycleCompleteEventArgs e);
        public event LearningCycleCompleteHandler OnLearningCycleComplete;

        public NeuralNetworkTrainer(IFeedForward feedForward, IBackPropagate backPropagate, INetworkLayers networkLayers)
        {
            _feedForward = feedForward;
            _backPropagate = backPropagate;
            _networkLayers = networkLayers;

        }

        public void Train(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            _networkLayers.Create(neuralNetwork,
                                  neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Input),
                                  neuralNetworkTrainModel.HiddenNeuronsCount,
                                  neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Output));

            neuralNetwork.MathFunctions = neuralNetworkTrainModel.MathFunctions;
            neuralNetwork.Name = neuralNetworkTrainModel.NeuronNetworkName;

            var j = 0;
            var leastError = 1d;
            do
            {
                OnLearningCycleStart?.Invoke(this, new LearningCycleStartEventArgs(++j));

                var innerLeastError = 0d;

                for (int i = 0; i < neuralNetworkTrainModel.ValuesCount; i++)
                {
                    _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                    var expectedValues = neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i);                    
                    _backPropagate.Compute(neuralNetwork, expectedValues);
                    innerLeastError = Math.Max(innerLeastError, GetMaxError(neuralNetwork.OutputNeurons));

                    OnSampleLearned?.Invoke(this, new SampleEventArgs(i,
                                                                      expectedValues,
                                                                      neuralNetwork.OutputNeurons.Select(x => x.Value).ToArray(),
                                                                      neuralNetwork.OutputNeurons.Select(x => x.Error).ToArray()
                                                                      )
                                           );
                }

                //TODO: This calculation of leastError is totally wrong, calculation should use Cross-Validation with 20% of input data...
                //      If we can't reach the goal, parameters should change (e.g. number of hidden layers and neurons...)
                leastError = Math.Min(leastError, innerLeastError);

                OnLearningCycleComplete?.Invoke(this, new LearningCycleCompleteEventArgs(j, leastError));

            } while (leastError > neuralNetworkTrainModel.AcceptedError);

        }

        private double GetMaxError(List<Neuron> neurons)
        {
            var maxError = 0d;
            foreach (var neuron in neurons)
            {
                maxError = Math.Max(maxError, Math.Abs(neuron.Error));
            }
            return maxError;
        }

    }
}
