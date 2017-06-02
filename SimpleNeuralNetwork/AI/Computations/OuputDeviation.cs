using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations
{
    public class OuputDeviation : IOuputDeviation
    {
        public double Compute(NeuralNetwork neuralNetwork, double[] outputData)
        {
            IMaths _mathMethods;
            if (neuralNetwork.MathFunctions == MathFunctions.Sigmoid)
                _mathMethods = new AI.Computations.Maths.Sigmoid();
            else
                _mathMethods = new AI.Computations.Maths.HyperTan();

            var outputSum = 0d;
            for (var i = 0; i < neuralNetwork.OutputNeurons.Count(); i++)
            {
                if (outputData[i] == 0)
                    outputSum += Math.Abs(outputData[i] - neuralNetwork.OutputNeurons.ElementAt(i).Value);
                else
                    outputSum += Math.Abs((outputData[i] - neuralNetwork.OutputNeurons.ElementAt(i).Value) / outputData[i]);
            }
            return outputSum / neuralNetwork.OutputNeurons.Count();
        }
    }
}
