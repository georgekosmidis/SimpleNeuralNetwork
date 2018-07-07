using SimpleNeuralNetwork.Brain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Computations.Maths
{
    public class HyperTan : IMaths
    {
        Random rnd = new Random(DateTime.Now.Ticks.GetHashCode());

        public double OutputMethod(double val)
        {
            if (val < -20.0)
                return -1.0; // approximation is correct to 30 decimals
            else if (val > 20.0)
                return 1.0;
            else return Math.Tanh(val);
        }

        public double DerivativeMethod(double val)
        {
            return (1 + val) * (1 - val);
        }

        public double Random()
        {
            return rnd.NextDouble() * 2 - 1;
        }
    }
}
