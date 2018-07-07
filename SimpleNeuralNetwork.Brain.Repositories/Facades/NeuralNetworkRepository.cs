using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Brain.Repositories.Interfaces;
using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Repositories.Facades
{
   public class NeuralNetworkRepository : INeuralNetworkRepository
    {
        IBrainRepository _brainRepository;

        public NeuralNetworkRepository(IBrainRepository brainRepository)
        {
            _brainRepository = brainRepository;
        }

        public NeuralNetwork Load(IProblem problem)
        {
            return _brainRepository.Load(problem.Get().NeuronNetworkName);
        }
        public void Save(NeuralNetwork neuralNetwork)
        {
            _brainRepository.Save(neuralNetwork.Name, neuralNetwork);
        }
    }
}
