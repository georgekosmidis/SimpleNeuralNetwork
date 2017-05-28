using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class AdditionTrainer : AbstactTrainer, ITrainer
    {
        //Input data, defines variables inserted into the system, second dimension defines number of input neurons, for this case 2
        protected override double[][] inputData { get; } = new double[][] {
                                new double[] { .1, .2 },
                                new double[] { .3, .1 },
                                new double[] { .1, .4 },
                                new double[] { .1, .1 }
                            };

        //Expected output data defines expected result, second dimension defines number of output neurons, for this case 1
        protected override double[][] resultsData { get; } = new double[][] {
                                   new double[] { .3 },
                                   new double[] { .4 },
                                   new double[] { .5 },
                                   new double[] { .2 },
                               };

        public AdditionTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle) : base(neuralNetworkCompute, filehandle)
        {

        }
    }
}
