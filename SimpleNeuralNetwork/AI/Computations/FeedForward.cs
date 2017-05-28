using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations
{
    public class FeedForward : IFeedForward
    {
        IMaths _maths;
        public FeedForward(IMaths maths)
        {
            _maths = maths;
        }
        public void Compute(NeuralNetwork neuralNetwork, double[] inputData)
        {

            for (var i = 0; i < neuralNetwork.InputNeurons.Count(); i++)
                neuralNetwork.InputNeurons[i].Value = inputData[i];

            foreach (var hiddenNeuron in neuralNetwork.HiddenNeurons)
            {
                var total = hiddenNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                hiddenNeuron.Value = _maths.OutputMethod(total);
            }

            foreach (var outputNeuron in neuralNetwork.OutputNeurons)
            {
                var total = outputNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                outputNeuron.Value = _maths.OutputMethod(total);
            }
        }
    }
}
