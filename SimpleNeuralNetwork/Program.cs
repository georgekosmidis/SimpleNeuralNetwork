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
#if !DEBUG
            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
            //Choose NN, and remember to change the values for the Input Neurons. 
            //*****************************************************************************
            ////LOTTO
            //var lottoInput = new double[49];
            //for (var i = 0; i < 49; i++)
            //    lottoInput[i] = i + 1;
            //Run(NeuralNetworkFactoryHelper.NetworkFor.Lotto, LottoWriteMatrix, lottoInput);
            //*****************************************************************************
            ////Add-Subtract
            //Run(NeuralNetworkFactoryHelper.NetworkFor.AddSubtract, DefaultWriteMatrix, 1, 2, 1);
            //*****************************************************************************
            ////XOR
            //Run(NeuralNetworkFactoryHelper.NetworkFor.XOR, DefaultWriteMatrix, 1, 1);
            //*****************************************************************************

            Console.ReadKey(true);

        }

        private static void Run(NeuralNetworkFactoryHelper.NetworkFor networkFor, Action<double[]> displayAction, params double[] values)
        {
            var factoryHelper = new NeuralNetworkFactoryHelper(trainedNetworksPath);
            factoryHelper.OnUpdateStatus += Factory_OnUpdateStatus;

            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factoryHelper.Train(networkFor);//,true as second argument to save the trained network
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Computation with the newly trained network:");
            var liveValues = new double[values.Length];
            values.CopyTo(liveValues, 0);
            var result = neuralNetwork.Run(liveValues);
            displayAction(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factoryHelper.Load(networkFor);
            Console.WriteLine("");
            Console.WriteLine("Computation with the saved trained network:");
            liveValues = new double[values.Length];
            values.CopyTo(liveValues, 0);
            result = neuralNetwork.Run(liveValues);
            displayAction(result);
        }

        private static void DefaultWriteMatrix(double[] result)
        {
            for (var i = 0; i < result.Length; i++)
                Console.WriteLine("Output Neuron " + (i + 1) + ": " + result[i].ToString("0.000", CultureInfo.InvariantCulture));

        }

        private static void LottoWriteMatrix(double[] result)
        {
            var numbers = new Dictionary<int, double>();

            for (var i = 0; i < result.Length; i++)
                numbers[i] = result[i];

            foreach (var number in numbers.OrderByDescending(x => x.Value))
            {
                var line = String.Format("Probability of number {0,2}: {1,10} ", (number.Key + 1), number.Value.ToString("00.000 %", CultureInfo.InvariantCulture));
                Console.WriteLine(line);
            }


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