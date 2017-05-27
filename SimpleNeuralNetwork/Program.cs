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
            //Train with few inputs and many training cycles

            //Input data, defines variables inserted into the system, second dimension defines number of input neurons, for this case 2
            var inputData = new double[][] {
                                new double[] { .1, .2 },
                                new double[] { .3, .1 },
                                new double[] { .1, .4 },
                                new double[] { .1, .1 }
                            };

            //Expected output data defines expected result, second dimension defines number of output neurons, for this case 1
            var resultsData = new double[][] {
                                   new double[] { .3 },
                                   new double[] { .4 },
                                   new double[] { .5 },
                                   new double[] { .2 },
                               };

            //Create a neuron network with 2 input neurons, 5 hidden neurons and 1 output neuron
            var neuronNetwork = new AI.NeuralNetwork(inputData[0].Length, 5, resultsData[0].Length);

            //Train
            var j = 0;
            var leastError = 1d;
            do//could be smaller but training data are few and makes no point...
            {
                Console.WriteLine("Iteration : " + (j++));
                Console.WriteLine("---------------------");

                var innerLeastError = 0d;
                for (int i = 0; i < inputData.Length; i++)
                {
                    neuronNetwork.FeedForward(inputData[i]);
                    neuronNetwork.BackPropagate(resultsData[i]);

                    //ouput has only one neuron
                    Console.Write("Output : " + neuronNetwork.outputNeurons[0].Value.ToString("0.000000") + " ");
                    Console.Write("Expected : " + resultsData[i][0].ToString("0.000000") + " ");
                    Console.WriteLine("Error : " + neuronNetwork.outputNeurons[0].Error.ToString("0.000000") + " ");

                    innerLeastError = Math.Max(innerLeastError, Math.Abs(neuronNetwork.outputNeurons[0].Error));
                }
                leastError = Math.Min(leastError, innerLeastError);

                Console.WriteLine("");
            } while (leastError > .00001);

            //Compute
            Console.WriteLine("");
            Console.WriteLine("Done after " + j + " iterations...");
            Console.WriteLine("");
            Console.WriteLine("Computation to add unknown variables (.3, .2)");
            Console.WriteLine("=================================");

            var result = neuronNetwork.Compute(new double[] { .3, .2 });
            Console.WriteLine(Math.Round(result[0], 1).ToString("0.0"));
            Console.ReadKey();

        }
    }
}