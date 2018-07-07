using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Computations
{
    public class FeedForward : IFeedForward
    {
        IMathFactory _mathFactory;

        public FeedForward(IMathFactory mathFactory)
        {
            _mathFactory = mathFactory;
        }

        public void Compute(NeuralNetwork neuralNetwork, double[] inputData)
        {
            var _mathMethods = _mathFactory.Get(neuralNetwork);

            var i = 0;
            foreach (var inputNeuron in neuralNetwork.InputNeurons)
                inputNeuron.Value = inputData[i++];

            foreach (var hiddenLayer in neuralNetwork.HiddenLayers)
            {
                foreach (var hiddenNeuron in hiddenLayer)
                {
                    var total = hiddenNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                    hiddenNeuron.Value = _mathMethods.OutputMethod(total);// + hiddenNeuron.Bias;
                }
            }

            foreach (var outputNeuron in neuralNetwork.OutputNeurons)
            {
                var total = outputNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                outputNeuron.Value = _mathMethods.OutputMethod(total);// + outputNeuron.Bias;
            }
        }
    }
}
