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
    public class NeuralNetworkTrainModelCreate
    {
        NeuralNetworkTrainModel neuralNetworkTrainModel;

        public NeuralNetworkTrainModelCreate()
        {
            neuralNetworkTrainModel = new NeuralNetworkTrainModel();
        }

        public NeuralNetworkTrainModelCreate AddInputNeuron(Action<NeuronValues> addValuesExpression)
        {
            var neuronModel = new Models.NeuronModel();
            var neronValue = new NeuronValues(neuronModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.InputNeurons.Add(neuronModel);

            neuralNetworkTrainModel.Divisor = Math.Max(neuralNetworkTrainModel.Divisor, neronValue.Divisor);

            return this;
        }

        public NeuralNetworkTrainModelCreate AddHiddenLayer(Action<HiddenLayers> addNeuronsExpression)
        {
            var hiddenLayer = new List<HiddenLayerModel>();
            var hiddenLayers = new HiddenLayers(hiddenLayer);
            addNeuronsExpression(hiddenLayers);
            
            neuralNetworkTrainModel.HiddenLayers.AddRange(hiddenLayer);

            return this;
        }

        public NeuralNetworkTrainModelCreate AddOutputNeuron(Action<NeuronValues> addValuesExpression)
        {
            var neuronModel = new Models.NeuronModel();
            var neronValue = new NeuronValues(neuronModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.OutputNeurons.Add(neuronModel);
            return this;
        }

        public NeuralNetworkTrainModelCreate AutoAdjustHiddenLayer()
        {
            neuralNetworkTrainModel.AutoAdjuctHiddenLayer = true;
            neuralNetworkTrainModel.HiddenLayers = new List<HiddenLayerModel>();
            return this;
        }
        //public NeuralNetworkTrainModelCreate SetHiddenNeurons(int hiddenNeurons)
        //{
        //    neuralNetworkTrainModel.HiddenNeuronsCount = hiddenNeurons;
        //    return this;
        //}

        public NeuralNetworkTrainModelCreate SetMathFunctions(MathFunctions mathFunctions)
        {
            neuralNetworkTrainModel.MathFunctions = mathFunctions;
            return this;
        }

        public NeuralNetworkTrainModelCreate SetAcceptedError(double error)
        {
            neuralNetworkTrainModel.AcceptedError = error;
            return this;
        }

        public NeuralNetworkTrainModelCreate SetNeuralNetworkName(string name)
        {
            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new InvalidOperationException("Neural Network Name must must be a valid filename!");
            neuralNetworkTrainModel.NeuronNetworkName = name;
            return this;
        }

        public NeuralNetworkTrainModel Get()
        {
            if (neuralNetworkTrainModel.NeuronNetworkName == null || neuralNetworkTrainModel.NeuronNetworkName?.Trim() == "")
                throw new InvalidOperationException("Neural Network must have a name!");

            if (!neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenLayers.Count() == 0)
                throw new InvalidOperationException("You have to set either auto-adjuct or hidden layers!");
            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenLayers.Count() > 0)
                throw new InvalidOperationException("You have to set either auto-adjuct or hidden layers!");

            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.MathFunctions != MathFunctions.Unknown)
                throw new InvalidOperationException("You cannot auto-adjuct the hidden layer AND set Math Functions!");

            if (neuralNetworkTrainModel.InputNeurons.Count() == 0)
                throw new InvalidOperationException("You need at least one input neuron in your model!");

            if (neuralNetworkTrainModel.OutputNeurons.Count() == 0)
                throw new InvalidOperationException("You need at least one output neuron in your model!");

            var valuesCount = neuralNetworkTrainModel.InputNeurons.First().Values.Count();
            foreach (var neuron in neuralNetworkTrainModel.InputNeurons)
            {
                if (valuesCount != neuron.Values.Count())
                    throw new InvalidOperationException("All neurons must have same count of values!");
            }

            if (neuralNetworkTrainModel.InputNeurons.SelectMany(x => x.Values).Count(x => x < 0) > 0 || neuralNetworkTrainModel.OutputNeurons.SelectMany(x => x.Values).Count(x => x < 0) > 0 )
                neuralNetworkTrainModel.MathFunctions = MathFunctions.HyperTan;
            else
                neuralNetworkTrainModel.MathFunctions = MathFunctions.Sigmoid;

            return neuralNetworkTrainModel;
        }


    }

}
