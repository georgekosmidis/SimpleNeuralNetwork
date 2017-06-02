using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface ITrainSet
    {
        void Train(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel);
    }
}