using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class CustomTrainer : AbstactTrainer, ITrainer
    {
        //*****************************************************************************
        //REMEMBER TO NORMALIZE YOUR DATA, 
        // VALUES MUST BE FROM -1 to +1 FOR HYPERTAN AND 0 TO 1 FOR SIGMOID
        //*****************************************************************************

        // Add your input data to the network
        // Input data, defines variables inserted into the system, second dimension defines number of input neurons, for this case 3
        protected override double[][] inputData { get; } = new double[][] {
                                new double[] { .2, .1, .1 },
                                new double[] { .3, .2, .1 },
                                new double[] { .2, .1, .2 },
                                new double[] { .1, .1, .1 }
                            };

        // Add your expected data to the network
        //Expected output data defines expected result, second dimension defines number of output neurons, for this case 2
        protected override double[][] resultsData { get; } = new double[][] {
                                   new double[] { 0, .4 },
                                   new double[] { 0, .6 },
                                   new double[] { -.1, .5 },
                                   new double[] { -.1, .3 }
                           };

        public CustomTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle) : base(neuralNetworkCompute, filehandle)
        {

        }
    }
}
