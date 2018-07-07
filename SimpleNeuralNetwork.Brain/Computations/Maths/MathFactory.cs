using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Computations.Maths
{
    public class MathFactory : IMathFactory
    {

        public IMaths Get(NeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.MathFunctions == eMathFunctions.Sigmoid)
                return new Brain.Computations.Maths.Sigmoid();
            else
                return new Brain.Computations.Maths.HyperTan();
        }
    }
}
