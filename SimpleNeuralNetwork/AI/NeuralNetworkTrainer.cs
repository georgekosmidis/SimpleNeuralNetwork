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
        private IOuputDeviation _ouputDeviation;

        private List<NeuralNetwork> _neuralNetworkSetup = new List<NeuralNetwork>();

        public delegate void LearningCycleCompleteHandler(object sender, LearningCycleCompleteEventArgs e);
        public event LearningCycleCompleteHandler OnLearningCycleComplete;
        public delegate void NetworkReconfiguredHandler(object sender, NetworkReconfiguredEventArgs e);
        public event NetworkReconfiguredHandler OnNetworkReconfigured;

        public NeuralNetworkTrainer(IFeedForward feedForward, IBackPropagate backPropagate, INetworkLayers networkLayers, IOuputDeviation ouputDeviation)
        {
            _feedForward = feedForward;
            _backPropagate = backPropagate;
            _networkLayers = networkLayers;
            _ouputDeviation = ouputDeviation;
        }

        public NeuralNetwork Train(NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            var neuralNetwork = _networkLayers.Create(neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Input),
                                                      neuralNetworkTrainModel.HiddenNeuronsCount,
                                                      neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Output),
                                                      neuralNetworkTrainModel.AutoAdjuctHiddenLayer);

            OnNetworkReconfigured?.Invoke(this, new NetworkReconfiguredEventArgs(neuralNetwork.HiddenNeurons.Count()));

            neuralNetwork.MathFunctions = neuralNetworkTrainModel.MathFunctions;
            neuralNetwork.Name = neuralNetworkTrainModel.NeuronNetworkName;

            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .7));
            var validationSetCount = Convert.ToInt32(Math.Floor((neuralNetworkTrainModel.ValuesCount - trainSetCount) * .7));
            var testSet = Convert.ToInt32(neuralNetworkTrainModel.ValuesCount - trainSetCount - validationSetCount);

            var iteration = 0;
            var lastOutputDeviation = double.MaxValue;
            while (++iteration < int.MaxValue)
            {
                //train
                for (var i = 0; i < trainSetCount; i++)
                {
                    _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                    var expectedValues = neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i);
                    _backPropagate.Compute(neuralNetwork, expectedValues);
                }

                //validate, should training stop?
                var innerLastOutputDeviation = 0d;
                for (var i = trainSetCount; i < trainSetCount + validationSetCount; i++)
                {
                    _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                    innerLastOutputDeviation += _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
                }

                //check deviation to break training
                innerLastOutputDeviation /= validationSetCount;
                if (lastOutputDeviation <= innerLastOutputDeviation || innerLastOutputDeviation < neuralNetworkTrainModel.AcceptedError)
                {
                    lastOutputDeviation = innerLastOutputDeviation;
                    break;
                }
                lastOutputDeviation = innerLastOutputDeviation;


                OnLearningCycleComplete?.Invoke(this, new LearningCycleCompleteEventArgs(iteration, lastOutputDeviation));
            }
            
            //store neural network
            neuralNetwork.NueralNetworkError = lastOutputDeviation;
            _neuralNetworkSetup.Add(neuralNetwork);

            //check if we have to reconfigure NN
            if (iteration < neuralNetwork.HiddenNeurons.Count() || (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && lastOutputDeviation > neuralNetworkTrainModel.AcceptedError))
            {
                if (neuralNetwork.HiddenNeurons.Count() < (neuralNetwork.InputNeurons.Count() + neuralNetwork.OutputNeurons.Count()) * 10)
                {
                    neuralNetwork = Train(neuralNetworkTrainModel);
                }
            }

            //choose best NN Setup
            neuralNetwork = _neuralNetworkSetup.OrderBy(x => x.NueralNetworkError).First();

            //test, find real life error
            var testError = 0d;
            for (var i = trainSetCount + validationSetCount; i < trainSetCount + validationSetCount + testSet; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                testError += _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
            }
            neuralNetwork.NueralNetworkError = testError / testSet;//update with test error

            return neuralNetwork;
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
