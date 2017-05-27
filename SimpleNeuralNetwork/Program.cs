using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random rnd = new Random();
            //var inputData2 = new double[10000][];
            //var resultsData2 = new double[10000][];
            //for (var i = 0; i < 10000; i++)
            //{
            //    var i1 = rnd.Next(1, 5) / 10d;
            //    var i2 = rnd.Next(1, 5) / 10d;
            //    var s = i1 + i2;
            //    inputData2[i] = new double[] { i1, i2 };
            //    resultsData2[i] = new double[] { s };
            //}

            //second dimension defines number of input neurons, for this case 3
            var inputData = new double[][] {
                                new double[] { .1, .2},
                                new double[] { .3, .1},
                                new double[] { .1, .4},
                                new double[] { .1, .1}
                            };

            //Training data defines expected result, first dimension defines number of output neurons 
            var resultsData = new double[][] {
                                   new double[] { .3 },
                                   new double[] { .4 },
                                   new double[] { .5 },
                                   new double[] { .2 }
                               };

            var neuronNetwork = new AI.NeuralNetwork(inputData, resultsData, 3);


            var TRAIN_FOR_INDEX = 0;
            var j = -1;
            while (++j < 100)
            {
                Console.WriteLine("Iteration : " + (j + 1));
                Console.WriteLine("---------------------");
                neuronNetwork.FeedForward(TRAIN_FOR_INDEX);
                neuronNetwork.BackPropagate();

                Console.Write("Output : " + neuronNetwork.outputNeurons[0].Output.ToString("0.00000") + " ");
                Console.WriteLine("Expected : " + neuronNetwork.outputNeurons[0].ExpectedOutput.ToString("0.00000") + " ");

                Console.WriteLine("");
            }

            Console.ReadKey();

        } // Program

    }
}