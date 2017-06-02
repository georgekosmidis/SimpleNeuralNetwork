using SimpleNeuralNetwork.EventArguments;
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
        private static string trainedNetworksPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;

        static void Main(string[] args)
        {
            //Choose NN, and remember to change the values for the Input Neurons
            Run(NeuralNetworkFactoryHelper.NetworkFor.AddSubtract, new double[] { .2, .2, .1 });

            Console.ReadKey(true);

        }

        private static void Run(NeuralNetworkFactoryHelper.NetworkFor networkFor, double[] tests)
        {
            var factoryHelper = new NeuralNetworkFactoryHelper(trainedNetworksPath);
            factoryHelper.OnUpdateStatus += Factory_OnUpdateStatus;

            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factoryHelper.Train(networkFor);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Computation with the newly trained network:");
            var result = neuralNetwork.Run(tests);
            WriteMatrix(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factoryHelper.Load(networkFor);
            Console.WriteLine("");
            Console.WriteLine("Computation with the saved trained network:");
            result = neuralNetwork.Run(tests);
            WriteMatrix(result);
        }

        private static void WriteMatrix(double[] result)
        {
            for (var i = 0; i < result.Length; i++)
                Console.WriteLine("Output Neuron " + (i + 1) + ": " + result[i].ToString("0.000", CultureInfo.InvariantCulture));

        }

        private static void Factory_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            Console.Write(e.Status);
        }

    }
}