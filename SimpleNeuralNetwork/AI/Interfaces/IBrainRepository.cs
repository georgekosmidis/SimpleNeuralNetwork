using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Modeling.Interfaces
{
    public interface IBrainRepository
    {
        NeuralNetwork Load(string fileName);
        void Save(string filePath, NeuralNetwork neuralNetwork);
    }
}