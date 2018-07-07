using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Trainer.EventArguments
{
    public class NetworkReconfiguredEventArgs : EventArgs
    {
        public List<int> HiddenLayers { get; }
        public NetworkReconfiguredEventArgs(List<int> hiddenLayers)
        {
            HiddenLayers = hiddenLayers;
        }
    }
}
