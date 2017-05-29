using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training
{
    public class NeuralNetworkModeling
    {
        NeuralNetworkTrainModel neuronsModel = new NeuralNetworkTrainModel();

        public NeuralNetworkModeling() { }


        public NeuralNetworkModeling AddInputNeuron()
        {
            neuronsModel.Add(new Models.NeuronTrainModel() { Layer = NeuronLayer.Input });
            return this;
        }

        public NeuralNetworkModeling AddOutputNeuron()
        {
            neuronsModel.Add(new Models.NeuronTrainModel() { Layer = NeuronLayer.Output });
            return this;
        }

        public NeuralNetworkModeling AddValue(double value)
        {
            neuronsModel.Last().Values.Add(value);
            return this;
        }
        public NeuralNetworkModeling SetHiddenNeurons(int hiddenNeurons)
        {
            neuronsModel.HiddenNeuronsCount = hiddenNeurons;
            return this;
        }

        public NeuralNetworkModeling SetMathFunctions(MathFunctions mathFunctions)
        {
            neuronsModel.MathFunctions = mathFunctions;
            return this;
        }

        public NeuralNetworkModeling SetAcceptedError(double error)
        {
            neuronsModel.AcceptedError = error;
            return this;
        }

        public NeuralNetworkTrainModel Get()
        {
            if (neuronsModel.Count(x => x.Layer == NeuronLayer.Input) == 0)
                throw new InvalidOperationException("You need at least one input neuron in your model!");
            if (neuronsModel.Count(x => x.Layer == NeuronLayer.Output) == 0)
                throw new InvalidOperationException("You need at least one output neuron in your model!");

            var valuesCount = neuronsModel.First().Values.Count();
            foreach (var neuron in neuronsModel)
                if (valuesCount != neuron.Values.Count())
                    throw new InvalidOperationException("All neurons must have same count of values!");


            return neuronsModel;
        }


    }

}
