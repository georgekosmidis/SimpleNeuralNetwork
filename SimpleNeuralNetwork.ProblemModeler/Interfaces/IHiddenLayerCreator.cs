using SimpleNeuralNetwork.ProblemModeler.Creators;

namespace SimpleNeuralNetwork.Modeler.Interfaces
{
    public interface IHiddenLayerCreator
    {
        IHiddenLayerCreator AddHiddenLayer(int neuronsCount);
    }
}