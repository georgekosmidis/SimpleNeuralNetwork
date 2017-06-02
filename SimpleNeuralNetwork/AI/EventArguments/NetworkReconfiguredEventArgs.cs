using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.EventArguments
{
    public class NetworkReconfiguredEventArgs : EventArgs
    {
        public int HiddenNeuronsCount { get; }
        public NetworkReconfiguredEventArgs(int hiddenNeuronsCount)
        {
            HiddenNeuronsCount = hiddenNeuronsCount;
        }
    }
}
