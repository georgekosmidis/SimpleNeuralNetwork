using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Helpers;
using SimpleNeuralNetwork.Interfaces;
using SimpleNeuralNetwork.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public NeuralNetworkCompute Get(NetworkFor networkFor, TrainType trainType, MathMethods mathMethods)
        {
            IMaths _mathMethods;
            if (mathMethods == MathMethods.Sigmoid)
                _mathMethods = new AI.Computations.Maths.Sigmoid();
            else
                _mathMethods = new AI.Computations.Maths.HyperTan();

            var neuralNetworkCompute = new NeuralNetworkCompute(
                                            new FeedForward(_mathMethods),
                                            new BackPropagate(_mathMethods),
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
            var dataHandle = new JsonFileHandle(
                                  _trainedNetworksPath
                              );
            ITrainer trainer;

            switch (networkFor)
            {
                case NetworkFor.Addition:
                    trainer = new Trainers.AdditionTrainer(
                             neuralNetworkCompute,
                             dataHandle
                         );
                    break;
                case NetworkFor.XOR:
                    trainer = new Trainers.XorTrainer(
                             neuralNetworkCompute,
                             dataHandle
                         );
                    break;
                case NetworkFor.Custom:
                    trainer = new Trainers.CustomTrainer(
                             neuralNetworkCompute,
                             dataHandle
                         );
                    break;
                default:
                    throw new NotImplementedException("Network " + networkFor + " not implemented!");
            }

            (trainer as AbstactTrainer).OnUpdateStatus += Factory_OnUpdateStatus;
            trainer.Train(5, .001);

            trainer.Save(networkFor + "Trainer.json");

            return neuralNetworkCompute;
        }

        private NeuralNetworkCompute GetTrained(NetworkFor networkFor, NeuralNetworkCompute neuralNetworkCompute)
        {

            new Trainers.TrainDataLoader(
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
