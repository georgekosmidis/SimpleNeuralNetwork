using SimpleNeuralNetwork.AI.Training.EventArguments;
using SimpleNeuralNetwork.Factories;
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
            Run(NeuralNetworkFactory.NetworkFor.Custom, new double[] { .2, .1, .1 });

            Console.ReadKey(true);

        }

        private static void Run(NeuralNetworkFactory.NetworkFor networkFor, double[] tests)
        {
            var factory = new NeuralNetworkFactory(trainedNetworksPath);
            factory.OnUpdateStatus += Factory_OnUpdateStatus;

            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factory.Get(networkFor, NeuralNetworkFactory.TrainType.LiveTraining);
            Console.WriteLine("");
            Console.WriteLine("Computation with newly trained network:");
            var result = neuralNetwork.Compute(tests);
            WriteMatrix(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factory.Get(networkFor, Factories.NeuralNetworkFactory.TrainType.Trained);

            Console.WriteLine("");
            Console.WriteLine("Computation with old trained network:");
            result = neuralNetwork.Compute(tests);
            WriteMatrix(result);
        }

        private static void WriteMatrix(double[] result)
        {
            for (var i = 0; i < result.Length; i++)
                Console.WriteLine("Output Neuron " + (i + 1) + ": " + Math.Round(result[i], 1).ToString("0.0", CultureInfo.InvariantCulture));

        }

        private static void Factory_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }

    }
}