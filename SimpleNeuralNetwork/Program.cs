using Newtonsoft.Json;
using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Factories;
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
            var factory = new NeuralNetworkFactory(trainedNetworksPath);
            factory.OnUpdateStatus += Factory_OnUpdateStatus;
            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factory.Get(NeuralNetworkFactory.NetworkFor.Addition, 
                                            NeuralNetworkFactory.TrainType.LiveTraining, 
                                            NeuralNetworkFactory.MathMethods.Sigmoid);
            Console.WriteLine("");
            Console.WriteLine("Computation with newly trained network:");
            var result = neuralNetwork.Compute(new double[] { .3, .2 });
            Console.WriteLine("f(.3, .2)=" + Math.Round(result[0], 1).ToString("0.0"));

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factory.Get(Factories.NeuralNetworkFactory.NetworkFor.Addition, 
                                        Factories.NeuralNetworkFactory.TrainType.Trained,
                                        NeuralNetworkFactory.MathMethods.Sigmoid);

            Console.WriteLine("");
            Console.WriteLine("Computation with old trained network:");
            result = neuralNetwork.Compute(new double[] { .3, .2 });
            Console.WriteLine("f(.3, .2)=" + Math.Round(result[0], 1).ToString("0.0"));

            Console.ReadKey(true);

        }

        private static void Factory_OnUpdateStatus(object sender, EventArgumens.ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }

    }
}