using SimpleNeuralNetwork.Models;
using System.Collections.Generic;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface INetworkLayers
    {
        NeuralNetwork Create(int inputNeuronsCount, List<int> hiddenLayers, int outputNeuronsCount, bool autoAdjustHiddenLayer);

    }
}