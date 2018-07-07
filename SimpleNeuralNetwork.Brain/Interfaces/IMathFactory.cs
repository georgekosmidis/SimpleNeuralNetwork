using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface IMathFactory
    {
        IMaths Get(NeuralNetwork neuralNetwork);
    }
}