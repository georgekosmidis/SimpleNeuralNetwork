using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers
{
    public class NeuralNetworkModeling
    {
        NeuralNetworkTrainModel neuralNetworkTrainModel = new NeuralNetworkTrainModel();

        public NeuralNetworkModeling() { }


        public NeuralNetworkModeling AddInputNeuron()
        {
            neuralNetworkTrainModel.Add(new Models.NeuronTrainModel() { Layer = NeuronLayer.Input });
            return this;
        }

        public NeuralNetworkModeling AddOutputNeuron()
        {
            neuralNetworkTrainModel.Add(new Models.NeuronTrainModel() { Layer = NeuronLayer.Output });
            return this;
        }

        public NeuralNetworkModeling AddValue(double value)
        {
            neuralNetworkTrainModel.Last().Values.Add(value);
            return this;
        }
        public NeuralNetworkModeling SetHiddenNeurons(int hiddenNeurons)
        {
            neuralNetworkTrainModel.HiddenNeuronsCount = hiddenNeurons;
            return this;
        }

        public NeuralNetworkModeling SetMathFunctions(MathFunctions mathFunctions)
        {
            neuralNetworkTrainModel.MathFunctions = mathFunctions;
            return this;
        }

        public NeuralNetworkModeling SetAcceptedError(double error)
        {
            neuralNetworkTrainModel.AcceptedError = error;
            return this;
        }

        public NeuralNetworkModeling SetNeuralNetworkName(string name)
        {
            if(name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new InvalidOperationException("Neural Network Name must must be a valid filename!");
            neuralNetworkTrainModel.NeuronNetworkName = name;
            return this;
        }

        public NeuralNetworkTrainModel Get()
        {
            if (neuralNetworkTrainModel.NeuronNetworkName == null || neuralNetworkTrainModel.NeuronNetworkName?.Trim() == "")
                throw new InvalidOperationException("Neural Network must have a name!");

            if (neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Input) == 0)
                throw new InvalidOperationException("You need at least one input neuron in your model!");

            if (neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Output) == 0)
                throw new InvalidOperationException("You need at least one output neuron in your model!");

            var valuesCount = neuralNetworkTrainModel.First().Values.Count();
            foreach (var neuron in neuralNetworkTrainModel)
                if (valuesCount != neuron.Values.Count())
                    throw new InvalidOperationException("All neurons must have same count of values!");


            return neuralNetworkTrainModel;
        }


    }

}
