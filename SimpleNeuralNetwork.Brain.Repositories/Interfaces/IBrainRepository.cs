using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Repositories.Interfaces
{
    public interface IBrainRepository
    {
        NeuralNetwork Load(string fileName);
        void Save(string filePath, NeuralNetwork neuralNetwork);
    }
}