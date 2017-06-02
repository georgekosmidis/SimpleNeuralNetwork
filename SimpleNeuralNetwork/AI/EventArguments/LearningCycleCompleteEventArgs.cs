using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.EventArguments
{
    public class LearningCycleCompleteEventArgs : EventArgs
    {
        public int Iteration { get; }
        public double Error { get; }
        public LearningCycleCompleteEventArgs(int iteration, double error)
        {
            Iteration = iteration;
            Error = error;
        }
    }
}
