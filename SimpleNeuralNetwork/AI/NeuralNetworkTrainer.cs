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

        public delegate void IterationUpdateHandler(object sender, IterationEventArgs e);
        public event IterationUpdateHandler OnNewIteration;
        public delegate void SampleLearnedHandler(object sender, SampleEventArgs e);
        public event SampleLearnedHandler OnSampleLearned;

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
                OnNewIteration?.Invoke(this, new IterationEventArgs(j + 1));

                var innerLeastError = 0d;

                for (int i = 0; i < neuralNetworkTrainModel.ValuesCount; i++)
                {
                    _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                    var expectedValues = neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i);                    
                    _backPropagate.Compute(neuralNetwork, expectedValues);
                    innerLeastError = Math.Max(innerLeastError, GetMaxError(neuralNetwork.OutputNeurons));

                    OnSampleLearned?.Invoke(this, new SampleEventArgs(i, 
                                                                      expectedValues,
                                                                      neuralNetwork.OutputNeurons.Select( x=> x.Value).ToArray(),
                                                                      neuralNetwork.OutputNeurons.Select(x => x.Error).ToArray()
                                                                      )
                                           );
                }
                leastError = Math.Min(leastError, innerLeastError);

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
