using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Facades
{
    public class NeuralNetworkRunner : INeuralNetworkRunner
    {
        private IFeedForward _feedForward;

        public NeuralNetworkRunner(IFeedForward feedForward)
        {
            _feedForward = feedForward;
        }

        public double[] Run(NeuralNetwork neuralNetwork, double[] inputData)
        {
            for (var i = 0; i < inputData.Length; i++)
                inputData[i] /= neuralNetwork.Divisor;
            _feedForward.Compute(neuralNetwork, inputData);

            return neuralNetwork.OutputNeurons.Select(a => a.Value * neuralNetwork.Divisor).ToArray();
        }
    }
}
