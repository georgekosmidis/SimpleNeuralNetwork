using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.EventArguments
{
    public class SampleEventArgs : EventArgs
    {
        public int NeuronIndex { get; }
        public double[] Expected { get; }
        public double[] Actual { get; }
        public double[] Error { get; }

        public SampleEventArgs(int neuronIndex, double[] expected, double[] actual, double[] error)
        {
            NeuronIndex = neuronIndex;
            Expected = expected;
            Actual = actual;
            Error = error;
        }
    }
}
