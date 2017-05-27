using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class Neuron
    {
        public Double Layer { get; set; }

        public Double Index { get; set; }

        public Double Output { get; private set; }

        public Double ExpectedOutput { get; set; }

        public Double Bias { get; set; }

        public Double Error { get; set; }

        private Double _input = 0;
        public Double Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                if (this.Layer == 0)
                    this.Output = value;
                else
                    this.Output = Sigmoid(value);
            }
        }

        private Double[] _weight = new Double[] { };
        public Double[] Weight { get; set; }

        public Neuron(Double layer, Double index)
        {
            this.Layer = layer;
            this.Index = index;
        }

        public Double Sigmoid(double val)
        {
            return 1.0 / (1.0 + Math.Exp(-val));
        }

        public Double SigmoidDerivative(double val)
        {
            return val * (1 - val);
        }
    }
}
