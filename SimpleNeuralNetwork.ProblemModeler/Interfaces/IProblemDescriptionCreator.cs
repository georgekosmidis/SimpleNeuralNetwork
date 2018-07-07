using System;
using SimpleNeuralNetwork.Models;
using SimpleNeuralNetwork.ProblemModeler.Creators;

namespace SimpleNeuralNetwork.Modeler.Interfaces
{
    public interface IProblemDescriptionCreator
    {
        IProblemDescriptionCreator AddHiddenLayers(Action<IHiddenLayerCreator> addNeuronsExpression);
        IProblemDescriptionCreator AddInputNeuron(Action<INeuronValuesCreator> addValuesExpression);
        IProblemDescriptionCreator AddOutputNeuron(Action<INeuronValuesCreator> addValuesExpression);
        IProblemDescriptionCreator AutoAdjustHiddenLayer();
        ProblemDescriptionModel Get();
        IProblemDescriptionCreator SetAcceptedError(double error);
        IProblemDescriptionCreator SetMathFunctions(eMathFunctions mathFunctions);
        IProblemDescriptionCreator SetNeuralNetworkName(string name);
    }
}