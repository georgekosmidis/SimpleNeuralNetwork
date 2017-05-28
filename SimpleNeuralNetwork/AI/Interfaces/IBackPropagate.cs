using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface IBackPropagate
    {
        void Compute(NeuralNetwork neuralNetwork, double[] outputData);
    }
}