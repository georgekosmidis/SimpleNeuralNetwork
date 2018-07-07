using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface IBackPropagate
    {
        void Compute(NeuralNetwork neuralNetwork, double[] outputData);
    }
}