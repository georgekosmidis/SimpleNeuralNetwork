using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface INeuralNetworkRunner
    {
        double[] Run(NeuralNetwork neuralNetwork, double[] inputData);
    }
}