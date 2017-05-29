using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Models
{
    public class NeuronTrainModel
    { 
        public NeuronLayer Layer { get; set; }
        public List<double> Values { get; } = new List<double>();
    }
}
