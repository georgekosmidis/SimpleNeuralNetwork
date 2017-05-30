using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
   public class NeuralNetworkRepository
    {
        IBrainRepository _brainRepository;

        public NeuralNetworkRepository(IBrainRepository brainRepository)
        {
            _brainRepository = brainRepository;
        }

        public NeuralNetwork Load(string name)
        {
            return _brainRepository.Load(name);
        }
        public void Save(NeuralNetwork neuralNetwork)
        {
            _brainRepository.Save(neuralNetwork.Name, neuralNetwork);
        }
    }
}
