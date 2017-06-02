using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface ITestSet
    {
        void Test(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel);
    }
}