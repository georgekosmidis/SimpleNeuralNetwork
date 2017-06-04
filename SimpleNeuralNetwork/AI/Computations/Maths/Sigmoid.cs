using SimpleNeuralNetwork.AI.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations.Maths
{
    public class Sigmoid : IMaths
    {
        Random rnd = new Random(DateTime.Now.Ticks.GetHashCode());

        public Double OutputMethod(double val)
        {
            if (val < -45.0)
                return 0.0;
            else if (val > 45.0)
                return 1.0;
            return 1.0 / (1.0 + Math.Exp(-val));

        }

        public Double DerivativeMethod(double val)
        {
            return val * (1 - val);
        }

        public double Random()
        {
            return rnd.NextDouble();
        }
    }
}
