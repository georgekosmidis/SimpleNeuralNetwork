using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public static class Maths
    {
        public static Double Sigmoid(double val)
        {
            if (val < -45.0)
            {
                return 0.0;
            }
            else if (val > 45.0)
            {
                return 1.0;
            }
            return 1.0 / (1.0 + Math.Exp(-val));

        }

        public static Double Derivative(double val)
        {
            return val * (1 - val);
        }
    }
}
