using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Models
{
    public class ProblemDescriptionModel
    {
        public string NeuronNetworkName { get; set; }

        public bool AutoAdjuctHiddenLayer { get; set; } = false;

        public eMathFunctions MathFunctions { get; set; } = eMathFunctions.Unknown;

        public double AcceptedError { get; set; } = .02;

        public double Divisor { get; set; }

        public List<ProblemDescriptionNeuronModel> InputNeurons { get; set; } = new List<ProblemDescriptionNeuronModel>();
        public List<ProblemDescriptionHiddenLayerModel> HiddenLayers { get; set; } = new List<ProblemDescriptionHiddenLayerModel>();
        public List<ProblemDescriptionNeuronModel> OutputNeurons { get; set; } = new List<ProblemDescriptionNeuronModel>();

        //TODO: get these out of here
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
