using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.BrainRepositories.JsonFileHelpers.Models
{
    public class SavedSynapsis
    {
        public double Weight { get; set; }

        public int Index { get; set; }

        public int FromNeuronIndex { get; set; }
        public int ToNeuronIndex { get; set; }
    }
}
