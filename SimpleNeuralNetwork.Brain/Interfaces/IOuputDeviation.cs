using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface IOuputDeviation
    {
        double Compute(NeuralNetwork neuralNetwork, double[] outputData);
    }
}