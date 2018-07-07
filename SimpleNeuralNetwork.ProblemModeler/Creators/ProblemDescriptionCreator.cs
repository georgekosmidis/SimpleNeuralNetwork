using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SimpleNeuralNetwork.Models;
using SimpleNeuralNetwork.Modeler.Interfaces;

namespace SimpleNeuralNetwork.ProblemModeler.Creators
{
    public class ProblemDescriptionCreator : IProblemDescriptionCreator
    {
        ProblemDescriptionModel neuralNetworkTrainModel;

        public ProblemDescriptionCreator()
        {
            neuralNetworkTrainModel = new ProblemDescriptionModel();
        }

        public IProblemDescriptionCreator AddInputNeuron(Action<INeuronValuesCreator> addValuesExpression)
        {
            var neuronModel = new ProblemDescriptionNeuronModel();
            var neronValue = new NeuronValuesCreator(neuronModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.InputNeurons.Add(neuronModel);

            neuralNetworkTrainModel.Divisor = Math.Max(neuralNetworkTrainModel.Divisor, neronValue.Divisor);

            return this;
        }

        public IProblemDescriptionCreator AddHiddenLayers(Action<IHiddenLayerCreator> addNeuronsExpression)
        {
            var hiddenLayer = new List<ProblemDescriptionHiddenLayerModel>();
            var hiddenLayers = new HiddenLayerCreator(hiddenLayer);
            addNeuronsExpression(hiddenLayers);
            
            neuralNetworkTrainModel.HiddenLayers.AddRange(hiddenLayer);

            return this;
        }

        public IProblemDescriptionCreator AddOutputNeuron(Action<INeuronValuesCreator> addValuesExpression)
        {
            var neuronModel = new ProblemDescriptionNeuronModel();
            var neronValue = new NeuronValuesCreator(neuronModel);
            addValuesExpression(neronValue);

            neuralNetworkTrainModel.OutputNeurons.Add(neuronModel);
            return this;
        }

        public IProblemDescriptionCreator AutoAdjustHiddenLayer()
        {
            neuralNetworkTrainModel.AutoAdjuctHiddenLayer = true;
            neuralNetworkTrainModel.HiddenLayers = new List<ProblemDescriptionHiddenLayerModel>();
            return this;
        }
        //public NeuralNetworkTrainModelCreate SetHiddenNeurons(int hiddenNeurons)
        //{
        //    neuralNetworkTrainModel.HiddenNeuronsCount = hiddenNeurons;
        //    return this;
        //}

        public IProblemDescriptionCreator SetMathFunctions(eMathFunctions mathFunctions)
        {
            neuralNetworkTrainModel.MathFunctions = mathFunctions;
            return this;
        }

        public IProblemDescriptionCreator SetAcceptedError(double error)
        {
            neuralNetworkTrainModel.AcceptedError = error;
            return this;
        }

        public IProblemDescriptionCreator SetNeuralNetworkName(string name)
        {
            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new InvalidOperationException("Neural Network Name must must be a valid filename!");
            neuralNetworkTrainModel.NeuronNetworkName = name;
            return this;
        }

        public ProblemDescriptionModel Get()
        {
            if (neuralNetworkTrainModel.NeuronNetworkName == null || neuralNetworkTrainModel.NeuronNetworkName?.Trim() == "")
                throw new InvalidOperationException("Neural Network must have a name!");

            if (!neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenLayers.Count() == 0)
                throw new InvalidOperationException("You have to set either auto-adjuct or hidden layers!");
            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.HiddenLayers.Count() > 0)
                throw new InvalidOperationException("You have to set either auto-adjuct or hidden layers!");

            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetworkTrainModel.MathFunctions != eMathFunctions.Unknown)
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
                neuralNetworkTrainModel.MathFunctions = eMathFunctions.HyperTan;
            else
                neuralNetworkTrainModel.MathFunctions = eMathFunctions.Sigmoid;

            return neuralNetworkTrainModel;
        }


    }

}
