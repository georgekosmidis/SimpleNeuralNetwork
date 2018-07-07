using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Repositories.Interfaces
{
    public interface INeuralNetworkRepository
    {
        NeuralNetwork Load(IProblem problem);
        void Save(NeuralNetwork neuralNetwork);
    }
}