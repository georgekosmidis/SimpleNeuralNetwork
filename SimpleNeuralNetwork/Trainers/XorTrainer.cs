using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class XorTrainer : AbstactTrainer, ITrainer
    {
        //Input data, defines variables inserted into the system, second dimension defines number of input neurons, for this case 2
        protected override double[][] inputData { get; } = new double[][] {
                                new double[] { 0, 0 },
                                new double[] { 1, 0 },
                                new double[] { 0, 1 },
                                new double[] { 1, 1 }
                            };

        //Expected output data defines expected result, second dimension defines number of output neurons, for this case 1
        protected override double[][] resultsData { get; } = new double[][] {
                                   new double[] { 0 },
                                   new double[] { 1 },
                                   new double[] { 1 },
                                   new double[] { 0 }
                           };

        public XorTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle) : base(neuralNetworkCompute, filehandle)
        {

        }
    }
}
