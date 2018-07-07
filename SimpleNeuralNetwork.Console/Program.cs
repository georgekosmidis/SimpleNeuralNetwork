using SimpleNeuralNetwork.Brain.Trainer.EventArguments;
using SimpleNeuralNetwork.Console.Helpers;
using SimpleNeuralNetwork.Factories;
using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SimpleNeuralNetwork.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //IProblemLotto, IProblemAddSubstract, IProblemCustom
            var neuralNetwork = ConsoleHelper.TrainAndReturnNetwork<IProblemAddSubtract>(false);//save when train
            // - OR -
            //var neuralNetwork = ConsoleHelper.LoadAndReturnNetwork<IProblemAddSubtract>();

            //AddSubstract input for answers
            var input = new double[] { 12, 25, 9 };
            //LOTTO input for answers
            //----------------------------
            //var input = new double[49];
            //for (var i = 0; i < 49; i++)
            //    lottoInput[i] = i + 1;

            //AddSubstract input for result
            //----------------------------


            var result = new NeuralNetworkRunnerFactory()
                .Get()
                .Run(neuralNetwork, input);

            for (var i = 0; i < result.Length; i++)
                System.Console.WriteLine("Output Neuron " + (i + 1) + ": " + result[i].ToString("0.000", CultureInfo.InvariantCulture));

            System.Console.ReadKey();
        }


        
    }
}
