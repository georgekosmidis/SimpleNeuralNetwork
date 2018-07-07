using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface IFeedForward
    {
        void Compute(NeuralNetwork neuralNetwork, double[] inputData);
    }
}