using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface IOuputDeviation
    {
        double Compute(NeuralNetwork neuralNetwork, double[] outputData);
    }
}