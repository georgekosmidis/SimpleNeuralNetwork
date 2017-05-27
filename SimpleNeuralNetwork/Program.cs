using Newtonsoft.Json;
using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork
{
    class Program
    {
        private static string trainedNetworksPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "TrainedNetworks";

        static void Main(string[] args)
        {


            //TRAIN A NUERAL NETWORK
            var neuralNetwork = TrainAndReturn_Addition(true);

            Console.WriteLine("");
            Console.WriteLine("Computation to add unknown variables (.3, .2)");
            Console.WriteLine("=============================================");
            var result = neuralNetwork.Compute(new double[] { .3, .2 });
            Console.WriteLine("Result of actual training: " + Math.Round(result[0], 1).ToString("0.0"));

            //LOAD A TRAINED NEURAL NETWORK
            neuralNetwork = LoadAndReturn_Addition();
            result = neuralNetwork.Compute(new double[] { .3, .2 });
            Console.WriteLine("Result of saved training: " + Math.Round(result[0], 1).ToString("0.0"));

            Console.ReadKey();

        }

        private static NeuralNetwork TrainAndReturn_Addition(bool saveTrainData)
        {
            var neuralNetwork = new NeuralNetwork();
            var trainer = new Trainers.AdditionTrainer(
                              neuralNetwork,
                              new JsonFileHandle(
                                  trainedNetworksPath
                              )
                          );
            trainer.OnUpdateStatus += Trainer_OnUpdateStatus;
            trainer.Train(5, .001);
            if (saveTrainData)
                trainer.Save("AdditionTrainer.json");

            return neuralNetwork;
        }

        private static NeuralNetwork LoadAndReturn_Addition()
        {
            return new Trainers.TrainDataLoader(
                                    new JsonFileHandle(
                                        trainedNetworksPath
                                    )
                                ).Load("AdditionTrainer.json");

        }

        private static void Trainer_OnUpdateStatus(object sender, EventArgumens.ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }
    }
}