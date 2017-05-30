using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.EventArguments
{
    public class LearningCycleStartEventArgs : EventArgs
    {
        public int Iteration { get; }
        public LearningCycleStartEventArgs(int iteration)
        {
            Iteration = iteration;
        }
    }
}
