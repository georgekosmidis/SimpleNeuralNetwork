using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INetworkLayers
    {
        void Create(NeuralNetwork neuralNetwork, int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount);
    }
}