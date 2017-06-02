using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface IValidationSet
    {
        bool StopIterations(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel);
        bool StopTraining(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel);
    }
}