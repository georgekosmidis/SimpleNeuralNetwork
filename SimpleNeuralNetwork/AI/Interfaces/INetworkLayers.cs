using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INetworkLayers
    {
        NeuralNetwork Create(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, bool autoAdjustHiddenLayer);

    }
}