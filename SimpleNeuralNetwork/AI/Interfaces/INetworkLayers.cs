using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;
using System.Collections.Generic;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INetworkLayers
    {
        NeuralNetwork Create(int inputNeuronsCount, List<HiddenLayerModel> hiddenLayers, int outputNeuronsCount, bool autoAdjustHiddenLayer);

    }
}