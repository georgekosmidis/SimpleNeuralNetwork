using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Trainer.Interfaces
{
    public interface ITestSet
    {
        void Test(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel);
    }
}