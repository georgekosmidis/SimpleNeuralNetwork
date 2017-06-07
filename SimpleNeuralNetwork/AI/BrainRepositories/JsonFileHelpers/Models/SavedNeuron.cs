using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.BrainRepositories.JsonFileHelpers.Models
{
    public class SavedNeuron
    {
        public Double Value { get; set; }
        public Double Error { get; set; }

        public int Layer { get; set; }
        public int Index { get; set; }

    }
}
