using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers
{
    public class NeuronValue
    {
        private NeuronTrainModel _neuronTrainModel;

        public NeuronValue(NeuronTrainModel neuronTrainModel)
        {
            _neuronTrainModel = neuronTrainModel;
        }

        public NeuronValue AddValues(params double[] values)
        {
            _neuronTrainModel.Values.AddRange(values);
            return this;
        }
    }
}
