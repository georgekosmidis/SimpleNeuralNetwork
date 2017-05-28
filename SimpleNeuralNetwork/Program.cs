using Newtonsoft.Json;
using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Factories;
using SimpleNeuralNetwork.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var result = neuralNetwork.Compute(new double[] { .2, .2 });
            WriteMatrix(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factory.Get(Factories.NeuralNetworkFactory.NetworkFor.Addition,
                                        Factories.NeuralNetworkFactory.TrainType.Trained,
                                        NeuralNetworkFactory.MathMethods.Sigmoid);

            Console.WriteLine("");
            Console.WriteLine("Computation with old trained network:");
            result = neuralNetwork.Compute(new double[] { .2, .2 });
            WriteMatrix(result);

            Console.ReadKey(true);

        }

        private static void WriteMatrix(double[] result)
        {
            for (var i = 0; i < result.Length; i++)
                Console.WriteLine("Output Neuron " + (i + 1) + ": " + result[i].ToString("0.0000", CultureInfo.InvariantCulture));

        }

        private static void Factory_OnUpdateStatus(object sender, EventArgumens.ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }

    }
}