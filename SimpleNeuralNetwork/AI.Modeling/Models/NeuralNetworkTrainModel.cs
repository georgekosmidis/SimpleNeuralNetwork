using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Models
{
    public class NeuralNetworkTrainModel : List<Models.NeuronModel>
    {
        public string NeuronNetworkName { get; set; }

        public int HiddenNeuronsCount { get; set; } = -1;
        public bool AutoAdjuctHiddenLayer { get; set; } = false;

        public MathFunctions MathFunctions { get; set; } = MathFunctions.Unknown;

        public double AcceptedError { get; set; } = .02;

        public double Divisor { get; set; }

        public int ValuesCount
        {
            get
            {
                return this.First().Values.Count();
            }

        }

        public double[] GetValuesForLayer(NeuronLayer layer, int cycle)
        {
            return this.Where(x => x.Layer == layer).Select(x => x.Values[cycle] / Divisor).ToArray();
        }

    }
}
