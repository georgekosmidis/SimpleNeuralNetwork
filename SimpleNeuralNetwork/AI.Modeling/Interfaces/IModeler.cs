using SimpleNeuralNetwork.AI.Modeling.Models;

namespace SimpleNeuralNetwork.AI.Modeling.Interfaces
{
    public interface IModeler
    {
        NeuralNetworkTrainModel NeuralNetworkModel { get; }
    }
}