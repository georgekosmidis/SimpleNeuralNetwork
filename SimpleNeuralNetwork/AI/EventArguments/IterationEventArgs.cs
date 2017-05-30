using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.EventArguments
{
    public class IterationEventArgs : EventArgs
    {
        public int Iteration { get; }
        public IterationEventArgs(int iteration)
        {
            Iteration = iteration;
        }
    }
}
