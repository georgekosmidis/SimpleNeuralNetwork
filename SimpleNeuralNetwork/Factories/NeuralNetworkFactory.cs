using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Helpers;
using SimpleNeuralNetwork.Interfaces;
using SimpleNeuralNetwork.AI.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.Training.Trainers;

namespace SimpleNeuralNetwork.Factories
{
    public class NeuralNetworkFactory
    {
        string _trainedNetworksPath;
        public NeuralNetworkFactory(string trainedNetworksPath)
        {
            _trainedNetworksPath = trainedNetworksPath;
        }
        public enum NetworkFor { Addition, XOR, Custom }
        public enum TrainType { LiveTraining, Trained }
        public enum MathMethods { Sigmoid, HyperTan }

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public NeuralNetworkCompute Get(NetworkFor networkFor, TrainType trainType)
        {

            var neuralNetworkCompute = new NeuralNetworkCompute(
                                            new FeedForward(),
                                            new BackPropagate(),
                                            new NetworkLayers(
                                                new NeuronCompute()
                                            )
                                        );

            if (trainType == TrainType.LiveTraining)
                return TrainAndReturn(networkFor, neuralNetworkCompute);
            return GetTrained(networkFor, neuralNetworkCompute);

        }
        private NeuralNetworkCompute TrainAndReturn(NetworkFor networkFor, NeuralNetworkCompute neuralNetworkCompute)
        {
            var dataHandle = new JsonFileHandle(_trainedNetworksPath);
            ITrainer trainer;

            switch (networkFor)
            {
                case NetworkFor.Addition:
                    trainer = new AdditionTrainer(neuralNetworkCompute, dataHandle);
                    break;
                case NetworkFor.XOR:
                    trainer = new XorTrainer(neuralNetworkCompute, dataHandle);
                    break;
                case NetworkFor.Custom:
                    trainer = new CustomTrainer(neuralNetworkCompute, dataHandle);
                    break;
                default:
                    throw new NotImplementedException("Network " + networkFor + " not implemented!");
            }

            (trainer as AbstactTrainer).OnUpdateStatus += Factory_OnUpdateStatus;
            trainer.Train();

            trainer.Save(networkFor + "Trainer.json");

            return neuralNetworkCompute;
        }

        private NeuralNetworkCompute GetTrained(NetworkFor networkFor, NeuralNetworkCompute neuralNetworkCompute)
        {

            new AI.Training.TrainDataLoader(
                neuralNetworkCompute,
                new JsonFileHandle(
                    _trainedNetworksPath
                )
            ).Load(networkFor + "Trainer.json");
            return neuralNetworkCompute;
        }

        private void Factory_OnUpdateStatus(object sender, EventArgumens.ProgressEventArgs e)
        {
            OnUpdateStatus?.Invoke(sender, e);
        }
    }
}
