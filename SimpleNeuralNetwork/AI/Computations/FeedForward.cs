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

        public FeedForward()
        {

        }

        public void Compute(NeuralNetwork neuralNetwork, double[] inputData)
        {
            IMaths _mathMethods;
            if (neuralNetwork.MathFunctions == MathFunctions.Sigmoid)
                _mathMethods = new AI.Computations.Maths.Sigmoid();
            else
                _mathMethods = new AI.Computations.Maths.HyperTan();

            var i = 0;
            foreach (var inputNeuron in neuralNetwork.InputNeurons)
                inputNeuron.Value = inputData[i++];

            foreach (var hiddenNeuron in neuralNetwork.HiddenNeurons)
            {
                var total = hiddenNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                hiddenNeuron.Value = _mathMethods.OutputMethod(total);
            }

            foreach (var outputNeuron in neuralNetwork.OutputNeurons)
            {
                var total = outputNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                outputNeuron.Value = _mathMethods.OutputMethod(total);
            }
        }
    }
}
