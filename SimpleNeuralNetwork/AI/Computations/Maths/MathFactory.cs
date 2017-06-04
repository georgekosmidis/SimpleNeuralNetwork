using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations.Maths
{
    public class MathFactory : IMathFactory
    {

        public IMaths Get(NeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.MathFunctions == MathFunctions.Sigmoid)
                return new AI.Computations.Maths.Sigmoid();
            else
                return new AI.Computations.Maths.HyperTan();
        }
    }
}
