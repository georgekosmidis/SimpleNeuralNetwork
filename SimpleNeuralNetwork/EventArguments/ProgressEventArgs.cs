using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.EventArguments
{
    public class ProgressEventArgs : EventArgs
    {
        public string Status { get; }

        public ProgressEventArgs(string status)
        {
            Status = status;
        }
    }
}
