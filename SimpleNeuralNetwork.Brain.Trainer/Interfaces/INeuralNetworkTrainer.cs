using SimpleNeuralNetwork.Brain.Trainer.EventArguments;
using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;
using System;

namespace SimpleNeuralNetwork.Brain.Trainer.Interfaces
{
    public interface INeuralNetworkTrainer
    {
        event EventHandler<LearningCycleCompleteEventArgs> OnLearningCycleComplete;
        event EventHandler<NetworkReconfiguredEventArgs> OnNetworkReconfigured;

        NeuralNetwork Train(IProblem problem);
    }
}