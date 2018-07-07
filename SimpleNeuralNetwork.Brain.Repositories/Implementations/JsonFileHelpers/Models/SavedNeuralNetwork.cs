using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Repositories.Implementations.JsonFileHelpers.Models
{
    public class SavedNeuralNetwork
    {
        public List<SavedNeuron> InputNeurons { get; set; } = new List<SavedNeuron>();
        public List<List<SavedNeuron>> HiddenLayers { get; set; } = new List<List<SavedNeuron>>();
        public List<SavedNeuron> OutputNeurons { get; set; } = new List<SavedNeuron>();

        public List<SavedSynapsis> Synapsis { get; set; } = new List<SavedSynapsis>();
        public List<SavedSynapsis> InputHiddenSynapsis { get; set; } = new List<SavedSynapsis>();
        public List<SavedSynapsis> HiddenOutputSynapsis { get; set; } = new List<SavedSynapsis>();

        public eMathFunctions MathFunctions { get; set; }

        public double Divisor { get; set; }
    }
}
