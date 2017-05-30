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

        public delegate void LearningCycleStartHandler(object sender, LearningCycleStartEventArgs e);
        public event LearningCycleStartHandler OnLearningCycleStart;
        public delegate void SampleLearnedHandler(object sender, SampleEventArgs e);
        public event SampleLearnedHandler OnSampleLearned;
        public delegate void LearningCycleCompleteHandler(object sender, LearningCycleCompleteEventArgs e);
        public event LearningCycleCompleteHandler OnLearningCycleComplete;

        public NeuralNetworkFactory(NeuralNetworkRepository neuralNetworkRepository, NeuralNetworkRunner neuralNetworkRunner, NeuralNetworkTrainer neuralNetworkTrainer)
        {
            _neuralNetwork = new NeuralNetwork();
            _neuralNetworkRepository = neuralNetworkRepository;
            _neuralNetworkRunner = neuralNetworkRunner;
            _neuralNetworkTrainer = neuralNetworkTrainer;

            _neuralNetworkTrainer.OnLearningCycleStart += _neuralNetworkTrainer_OnLearningCycleStart;
            _neuralNetworkTrainer.OnLearningCycleComplete += _neuralNetworkTrainer_OnLearningCycleComplete;
            _neuralNetworkTrainer.OnSampleLearned += _neuralNetworkTrainer_OnSampleLearned;
        }

        private void _neuralNetworkTrainer_OnLearningCycleComplete(object sender, LearningCycleCompleteEventArgs e)
        {
            OnLearningCycleComplete?.Invoke(sender, e);
        }

        private void _neuralNetworkTrainer_OnLearningCycleStart(object sender, LearningCycleStartEventArgs e)
        {
            OnLearningCycleStart?.Invoke(sender, e);
        }

        private void _neuralNetworkTrainer_OnSampleLearned(object sender, SampleEventArgs e)
        {
            OnSampleLearned?.Invoke(sender, e);
        }


        //public double[] Run(double[] inputSample)
        //{
        //    return _neuralNetworkRunner.Run(_neuralNetwork, inputSample);
        //}

        public Runner Train(NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            _neuralNetworkTrainer.Train(_neuralNetwork, neuralNetworkTrainModel);
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
            NeuralNetwork _neuralNetwork;

            public Runner(NeuralNetwork neuralNetwork, NeuralNetworkRunner neuralNetworkRunner)
            {
                _neuralNetworkRunner = neuralNetworkRunner;
                _neuralNetwork = neuralNetwork;
            }
            public double[] Run(double[] inputSample)
            {
                return _neuralNetworkRunner.Run(_neuralNetwork, inputSample);
            }

        }
    }
}
