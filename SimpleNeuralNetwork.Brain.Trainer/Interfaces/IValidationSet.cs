using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Trainer.Interfaces
{
    public interface IValidationSet
    {
        bool StopIterations(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel);
        bool StopTraining(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel);
    }
}