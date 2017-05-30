using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.Training.Trainers;
using SimpleNeuralNetwork.AI.Training.Interfaces;
using SimpleNeuralNetwork.AI.Training.EventArguments;
using SimpleNeuralNetwork.AI.Training.Repositories;

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

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public NeuralNetworkCompute Get(NetworkFor networkFor, TrainType trainType)
        {

            var neuralNetworkCompute = new NeuralNetworkCompute(
                                            new FeedForward(),
                                            new BackPropagate(),
                                            new NetworkLayers(
                                                new NeuronSynapsis()
                                            )
                                        );

            if (trainType == TrainType.LiveTraining)
                return TrainAndReturn(networkFor, neuralNetworkCompute);
            return GetTrained(networkFor, neuralNetworkCompute);

        }
        private NeuralNetworkCompute TrainAndReturn(NetworkFor networkFor, NeuralNetworkCompute neuralNetworkCompute)
        {
            var dataHandle = new JsonFile(_trainedNetworksPath);
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

            new TrainedNetworksLoader(
                neuralNetworkCompute,
                new JsonFile(
                    _trainedNetworksPath
                )
            ).Load(networkFor + "Trainer.json");
            return neuralNetworkCompute;
        }

        private void Factory_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            OnUpdateStatus?.Invoke(sender, e);
        }
    }
}
