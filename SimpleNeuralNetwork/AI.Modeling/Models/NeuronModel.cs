using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Models
{
    public class NeuronModel
    { 
        public NeuronLayer Layer { get; set; }
        public List<double> Values { get; } = new List<double>();
    }
}
