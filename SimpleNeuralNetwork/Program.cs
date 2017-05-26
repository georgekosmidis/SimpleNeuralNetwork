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
                                new double[] { .1, .2 },
                                new double[] { .2, .3 },
                                new double[] { .3, .1 }
                            };

            //Training data defines expected result, second dimension defines the number of output neurons 
            var resultsData = new double[][] {
                                   new double[] { .3 },
                                   new double[] { .5 },
                                   new double[] { .4 }

                               };

            var neuronNetwork = new AI.NeuralNetwork(inputData, resultsData, 3);



            var j = 0;
            while (j++ < 10000)
            {
                neuronNetwork.FeedForward(0);
                neuronNetwork.BackPropagate();
            }


            var outputNeurons = neuronNetwork.Guess(new double[] { .1, .2 });

            Console.WriteLine("Requested Result:" + outputNeurons[0].Output);
            Console.ReadKey();

        } // Program

    }
}