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
        private ITrainSet _trainSet;
        private INetworkLayers _networkLayers;
        private IValidationSet _validationSet;
        private ITestSet _testSet;


        private List<NeuralNetwork> _neuralNetworkSetup = new List<NeuralNetwork>();

        public delegate void LearningCycleCompleteHandler(object sender, LearningCycleCompleteEventArgs e);
        public event LearningCycleCompleteHandler OnLearningCycleComplete;
        public delegate void NetworkReconfiguredHandler(object sender, NetworkReconfiguredEventArgs e);
        public event NetworkReconfiguredHandler OnNetworkReconfigured;

        public NeuralNetworkTrainer(INetworkLayers networkLayers, ITrainSet trainSet, IValidationSet validationSet, ITestSet testSet)
        {
            _networkLayers = networkLayers;
            _trainSet = trainSet;
            _validationSet = validationSet;
            _testSet = testSet;
        }

        public NeuralNetwork Train(NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            var neuralNetwork = _networkLayers.Create(neuralNetworkTrainModel.InputNeurons.Count(),
                                                      neuralNetworkTrainModel.HiddenLayers,
                                                      neuralNetworkTrainModel.OutputNeurons.Count(),
                                                      neuralNetworkTrainModel.AutoAdjuctHiddenLayer);

            OnNetworkReconfigured?.Invoke(this, new NetworkReconfiguredEventArgs(neuralNetwork.HiddenLayers.Aggregate(new List<int>(), (list, layer) => { list.Add(layer.Count()); return list; })));

            neuralNetwork.MathFunctions = neuralNetworkTrainModel.MathFunctions;
            neuralNetwork.Name = neuralNetworkTrainModel.NeuronNetworkName;
            neuralNetwork.Divisor = neuralNetworkTrainModel.Divisor;

            var iteration = 0;
            while (++iteration < int.MaxValue)
            {
                //train
                _trainSet.Train(neuralNetwork, neuralNetworkTrainModel);

                OnLearningCycleComplete?.Invoke(this, new LearningCycleCompleteEventArgs(iteration, neuralNetwork.NeuralNetworkError));

                //can NN train any more?
                if (_validationSet.StopIterations(neuralNetwork, neuralNetworkTrainModel))
                    break;

            }

            //store NN
            _neuralNetworkSetup.Add(neuralNetwork);

            //check if we have to reconfigure or retrain NN
            if (!_validationSet.StopTraining(neuralNetwork, neuralNetworkTrainModel))
                neuralNetwork = Train(neuralNetworkTrainModel);

            //choose best NN Setup
            neuralNetwork = _neuralNetworkSetup.OrderBy(x => x.NeuralNetworkError).First();

            //test, find real life error
            _testSet.Test(neuralNetwork, neuralNetworkTrainModel);

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
