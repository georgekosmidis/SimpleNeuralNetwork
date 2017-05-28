using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface IFeedForward
    {
        void Compute(NeuralNetwork neuralNetwork, double[] inputData);
    }
}