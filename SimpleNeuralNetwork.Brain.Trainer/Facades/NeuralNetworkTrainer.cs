using SimpleNeuralNetwork.Brain.Trainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Brain.Trainer.EventArguments;
using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using SimpleNeuralNetwork.Modeler.Interfaces;

namespace SimpleNeuralNetwork.Brain.Trainer.Facades
{
    public class NeuralNetworkTrainer : INeuralNetworkTrainer
    {
        private ITrainSet _trainSet;
        private INetworkLayers _networkLayers;
        private IValidationSet _validationSet;
        private ITestSet _testSet;


        private List<NeuralNetwork> _neuralNetworkSetup = new List<NeuralNetwork>();

        public event EventHandler<LearningCycleCompleteEventArgs> OnLearningCycleComplete;
        public event EventHandler<NetworkReconfiguredEventArgs> OnNetworkReconfigured;

        public NeuralNetworkTrainer(INetworkLayers networkLayers, ITrainSet trainSet, IValidationSet validationSet, ITestSet testSet)
        {
            _networkLayers = networkLayers;
            _trainSet = trainSet;
            _validationSet = validationSet;
            _testSet = testSet;
        }


        public NeuralNetwork Train(IProblem problem)
        {
            var description = problem.Get();

            var neuralNetwork = _networkLayers.Create(description.InputNeurons.Count(),
                                                      description.HiddenLayers.Select(x => x.NeuronsCount).ToList(),
                                                      description.OutputNeurons.Count(),
                                                      description.AutoAdjuctHiddenLayer);

            OnNetworkReconfigured?.Invoke(this, new NetworkReconfiguredEventArgs(neuralNetwork.HiddenLayers.Aggregate(new List<int>(), (list, layer) => { list.Add(layer.Count()); return list; })));

            neuralNetwork.MathFunctions = description.MathFunctions;
            neuralNetwork.Name = description.NeuronNetworkName;
            neuralNetwork.Divisor = description.Divisor;

            var iteration = 0;
            while (++iteration < int.MaxValue)
            {
                //train
                _trainSet.Train(neuralNetwork, description);

                //can NN train any more?
                if (_validationSet.StopIterations(neuralNetwork, description))
                {
                    OnLearningCycleComplete?.Invoke(this, new LearningCycleCompleteEventArgs(iteration, neuralNetwork.NeuralNetworkError));
                    break;
                }
                OnLearningCycleComplete?.Invoke(this, new LearningCycleCompleteEventArgs(iteration, neuralNetwork.NeuralNetworkError));

                //neuralNetwork.LearningRate = neuralNetwork.LearningRate * 1d / (iteration + 1);
            }

            //store NN
            _neuralNetworkSetup.Add(neuralNetwork);
            _neuralNetworkSetup = _neuralNetworkSetup.OrderBy(x => x.NeuralNetworkError).Take(5).ToList();

            //check if we have to reconfigure or retrain NN
            if (!_validationSet.StopTraining(neuralNetwork, description))
                neuralNetwork = Train(problem);

            //choose best NN Setup
            neuralNetwork = _neuralNetworkSetup.OrderBy(x => x.NeuralNetworkError).First();

            //test, find real life error
            _testSet.Test(neuralNetwork, description);

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
