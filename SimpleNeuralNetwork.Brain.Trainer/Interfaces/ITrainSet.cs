using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Trainer.Interfaces
{
    public interface ITrainSet
    {
        void Train(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel);
    }
}