using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Training.Interfaces
{
    public interface IDataRepository
    {
        NeuralNetwork Load(string fileName);
        void Save(string filePath, NeuralNetwork neuralNetwork);
    }
}