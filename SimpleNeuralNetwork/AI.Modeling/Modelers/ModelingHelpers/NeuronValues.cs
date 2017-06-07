using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers
{
    public class NeuronValues
    {
        private NeuronModel _neuronModel;
        public double Divisor { get; private set; }

        public NeuronValues(NeuronModel neuronModel)
        {
            _neuronModel = neuronModel;
        }

        public NeuronValues AddValues(params double[] values)
        {
            foreach (var value in values)
            {
                var divisor = Math.Pow(10, Math.Round(value).ToString().Length);
                Divisor =  Math.Max(Divisor, divisor);
            }

            _neuronModel.Values.AddRange(values);
            return this;
        }
    }
}
