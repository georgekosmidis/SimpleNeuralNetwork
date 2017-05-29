using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.Interfaces
{
    public interface ITrainer
    {
        void Save(string filename);
        void Train();
    }
}