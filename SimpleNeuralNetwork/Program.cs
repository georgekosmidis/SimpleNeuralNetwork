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
            //second dimension defines number of input neurons, for this case 3
            var inputData = new double[][] {
                                new double[] { 0.1, 0.2 },
                                new double[] { 0.2, 0.3 },
                                new double[] { 0.3, 0.5 },
                                new double[] { 0.3, 0.1 }
                            };

            //Training data defines expected result, second dimension defines the number of output neurons 
            var resultsData = new double[][] {
                                   new double[] { 0.3 },
                                   new double[] { 0.5 },
                                   new double[] { 0.8 },
                                   new double[] { 0.4 }

                               };

            var neuronNetwork = new AI.NeuralNetwork(inputData, resultsData, 10);

            var j = 0;
            while (j++ < 10000)
            {
                for (var i = 0; i < inputData.Length; i++)
                {
                    neuronNetwork.FeedForward(i);
                    neuronNetwork.BackPropagate();
                }
            }

            var outputNeurons = neuronNetwork.Guess(new double[] { 0.2, 0.3 });

            return;

        } // Program

    }
}