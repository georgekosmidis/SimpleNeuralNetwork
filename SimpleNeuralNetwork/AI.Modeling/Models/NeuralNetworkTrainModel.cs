using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Models
{
    public class NeuralNetworkTrainModel
    {
        public string NeuronNetworkName { get; set; }

        public bool AutoAdjuctHiddenLayer { get; set; } = false;

        public MathFunctions MathFunctions { get; set; } = MathFunctions.Unknown;

        public double AcceptedError { get; set; } = .02;

        public double Divisor { get; set; }

        public List<Models.NeuronModel> InputNeurons { get; set; } = new List<Models.NeuronModel>();
        public List<Models.HiddenLayerModel> HiddenLayers { get; set; } = new List<Models.HiddenLayerModel>();
        public List<Models.NeuronModel> OutputNeurons { get; set; } = new List<Models.NeuronModel>();

        public int ValuesCount
        {
            get
            {
                return this.InputNeurons.First().Values.Count();
            }

        }

        public double[] GetInputValues(int cycle)
        {
            return InputNeurons.Select(x => x.Values[cycle] / Divisor).ToArray();
        }
        public double[] GetOutputValues(int cycle)
        {
            return OutputNeurons.Select(x => x.Values[cycle] / Divisor).ToArray();
        }
    }
}
