using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using System.ComponentModel;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers
{
    public class NeuralNetworkModeling
    {
        NeuralNetworkTrainModel neuralNetworkTrainModel = new NeuralNetworkTrainModel();

        public NeuralNetworkModeling() { }

        public NeuralNetworkModeling AddInputNeuron(Action<NeuronValue> addValuesExpression)
        {
            var neuronTrainModel =  new Models.NeuronTrainModel() { Layer = NeuronLayer.Input };
            var neronValue = new NeuronValue(neuronTrainModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.Add(neuronTrainModel);
                       
            return this;
        }
        public NeuralNetworkModeling AddOutputNeuron(Action<NeuronValue> addValuesExpression)
        {
            var neuronTrainModel = new Models.NeuronTrainModel() { Layer = NeuronLayer.Output };
            var neronValue = new NeuronValue(neuronTrainModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.Add(neuronTrainModel);
            return this;
        }

        public NeuralNetworkModeling AutoAdjustHiddenLayer()
        {
            neuralNetworkTrainModel.AutoAdjuctHiddenLayer = true;
            neuralNetworkTrainModel.HiddenNeuronsCount = -1;
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

            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenNeuronsCount > -1)
                throw new InvalidOperationException("You cannot auto-adjuct the hidden layer AND add hidden neurons!");

            if (neuralNetworkTrainModel.Count(x => x.Layer == NeuronLayer.Input) == 0)
                throw new InvalidOperationException("You need at least one input neuron in your model!");

            if (!neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenNeuronsCount < 1)
                throw new InvalidOperationException("You have to set either auto-adjuct or hidden neurons!");

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
