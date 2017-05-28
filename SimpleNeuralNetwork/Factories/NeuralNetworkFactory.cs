using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Helpers;
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
        public enum NetworkFor { Addition }
        public enum TrainType { LiveTraining, Trained }
        public enum MathMethods { Sigmoid, HyperTan }

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public NeuralNetworkCompute Get(NetworkFor networkFor, TrainType trainType, MathMethods maths)
        {
            IMaths math;
            if (maths == MathMethods.Sigmoid)
                math = new AI.Computations.Maths.Sigmoid();
            else
                math = new AI.Computations.Maths.HyperTan();

            var neuralNetworkCompute = new NeuralNetworkCompute(
                                            new FeedForward(math),
                                            new BackPropagate(math),
                                            new NetworkLayers(
                                                new NeuronCompute()
                                            )
                                        );
            switch (networkFor)
            {
                case NetworkFor.Addition:
                    return trainType == TrainType.LiveTraining ? Addition_Train(neuralNetworkCompute) : Addition_Trained(neuralNetworkCompute);
            }
            return null;
        }
        private NeuralNetworkCompute Addition_Train(NeuralNetworkCompute neuralNetworkCompute)
        {
            var trainer = new Trainers.AdditionTrainer(
                              neuralNetworkCompute,
                              new JsonFileHandle(
                                  _trainedNetworksPath
                              )
                          );
            trainer.OnUpdateStatus += Factory_OnUpdateStatus;
            trainer.Train(5, .001);

            trainer.Save("AdditionTrainer.json");

            return neuralNetworkCompute;
        }

        private NeuralNetworkCompute Addition_Trained(NeuralNetworkCompute neuralNetworkCompute)
        {
            new Trainers.TrainDataLoader(
                neuralNetworkCompute,
                new JsonFileHandle(
                    _trainedNetworksPath
                )
            ).Load("AdditionTrainer.json");
            return neuralNetworkCompute;
        }

        private void Factory_OnUpdateStatus(object sender, EventArgumens.ProgressEventArgs e)
        {
            OnUpdateStatus?.Invoke(sender, e);
        }
    }
}
