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
    public class NeuralNetworkFactory
    {
        NeuralNetwork _neuralNetwork;
        NeuralNetworkRepository _neuralNetworkRepository;
        NeuralNetworkRunner _neuralNetworkRunner;
        NeuralNetworkTrainer _neuralNetworkTrainer;

        public delegate void NetworkReconfiguredHandler(object sender, NetworkReconfiguredEventArgs e);
        public event NetworkReconfiguredHandler OnNetworkReconfigured;
        public delegate void LearningCycleCompleteHandler(object sender, LearningCycleCompleteEventArgs e);
        public event LearningCycleCompleteHandler OnLearningCycleComplete;

        public NeuralNetworkFactory(NeuralNetworkRepository neuralNetworkRepository, NeuralNetworkRunner neuralNetworkRunner, NeuralNetworkTrainer neuralNetworkTrainer)
        {
            _neuralNetwork = new NeuralNetwork();
            _neuralNetworkRepository = neuralNetworkRepository;
            _neuralNetworkRunner = neuralNetworkRunner;
            _neuralNetworkTrainer = neuralNetworkTrainer;

            _neuralNetworkTrainer.OnNetworkReconfigured += _neuralNetworkTrainer_OnNetworkReconfigured;
            _neuralNetworkTrainer.OnLearningCycleComplete += _neuralNetworkTrainer_OnLearningCycleComplete;
        }

        private void _neuralNetworkTrainer_OnNetworkReconfigured(object sender, NetworkReconfiguredEventArgs e)
        {
            OnNetworkReconfigured?.Invoke(sender, e);
        }

        private void _neuralNetworkTrainer_OnLearningCycleComplete(object sender, LearningCycleCompleteEventArgs e)
        {
            OnLearningCycleComplete?.Invoke(sender, e);
        }


        public Runner Train(NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            _neuralNetwork = _neuralNetworkTrainer.Train(neuralNetworkTrainModel);
            return new Runner(_neuralNetwork, _neuralNetworkRunner);
        }

        public void Save()
        {
            _neuralNetworkRepository.Save(_neuralNetwork);
        }

        public Runner Load(string name)
        {
            _neuralNetwork = _neuralNetworkRepository.Load(name);
            return new Runner(_neuralNetwork, _neuralNetworkRunner);
        }

        public class Runner
        {
            NeuralNetworkRunner _neuralNetworkRunner;
            public NeuralNetwork NeuralNetwork { get; private set; }

            public Runner(NeuralNetwork neuralNetwork, NeuralNetworkRunner neuralNetworkRunner)
            {
                _neuralNetworkRunner = neuralNetworkRunner;
                NeuralNetwork = neuralNetwork;
            }
            public double[] Run(double[] inputSample)
            {
                return _neuralNetworkRunner.Run(NeuralNetwork, inputSample);
            }

        }
    }
}
