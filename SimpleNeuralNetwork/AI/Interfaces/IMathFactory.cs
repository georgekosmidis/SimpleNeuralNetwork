using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface IMathFactory
    {
        IMaths Get(NeuralNetwork neuralNetwork);
    }
}