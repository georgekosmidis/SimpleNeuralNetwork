using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.ProblemModeler.Creators
{
    public class NeuronValuesCreator : INeuronValuesCreator
    {
        private ProblemDescriptionNeuronModel _neuronModel;
        public double Divisor { get; private set; }

        public NeuronValuesCreator(ProblemDescriptionNeuronModel neuronModel)
        {
            _neuronModel = neuronModel;
        }

        public INeuronValuesCreator AddValues(params double[] values)
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
