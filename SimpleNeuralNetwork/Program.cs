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
            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Choose NN, and remember to change the values for the Input Neurons. 
            //Bigger Numbers require, bigger dataset and alot more training time
            Run(NeuralNetworkFactoryHelper.NetworkFor.AddSubtract, 2, 1, 1);

            Console.ReadKey(true);

        }

        private static void Run(NeuralNetworkFactoryHelper.NetworkFor networkFor, params double[] values)
        {
            var factoryHelper = new NeuralNetworkFactoryHelper(trainedNetworksPath);
            factoryHelper.OnUpdateStatus += Factory_OnUpdateStatus;

            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factoryHelper.Train(networkFor, false);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Computation with the newly trained network:");
            var liveValues = new double[values.Length];
            values.CopyTo(liveValues, 0);
            var result = neuralNetwork.Run(liveValues);
            WriteMatrix(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factoryHelper.Load(networkFor);
            Console.WriteLine("");
            Console.WriteLine("Computation with the saved trained network:");
            liveValues = new double[values.Length];
            values.CopyTo(liveValues, 0);
            result = neuralNetwork.Run(liveValues);
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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(new String('*', 50));
            Console.WriteLine("Exception:");
            Console.WriteLine(((Exception)e.ExceptionObject).Message);
            Console.WriteLine(new String('*', 50));
            Console.ReadLine();
            Environment.Exit(1);
        }


    }
}