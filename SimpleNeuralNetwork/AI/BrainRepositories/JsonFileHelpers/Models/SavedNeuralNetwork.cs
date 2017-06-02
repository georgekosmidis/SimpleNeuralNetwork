using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.BrainRepositories.JsonFileHelpers.Models
{
    public class SavedNeuralNetwork
    {
        public List<SavedNeuron> InputNeurons { get; set; } = new List<SavedNeuron>();
        public List<SavedNeuron> HiddenNeurons { get; set; } = new List<SavedNeuron>();
        public List<SavedNeuron> OutputNeurons { get; set; } = new List<SavedNeuron>();

        public List<SavedSynapsis> InputHiddenSynapsis { get; set; } = new List<SavedSynapsis>();
        public List<SavedSynapsis> HiddenOutputSynapsis { get; set; } = new List<SavedSynapsis>();

        public MathFunctions MathFunctions { get; set; }

    }
}
