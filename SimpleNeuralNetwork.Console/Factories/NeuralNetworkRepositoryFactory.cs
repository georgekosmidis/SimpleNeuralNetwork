using SimpleNeuralNetwork.Brain.Repositories.Facades;
using SimpleNeuralNetwork.Brain.Repositories.Implementations;
using SimpleNeuralNetwork.Brain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Factories
{
    public class NeuralNetworkRepositoryFactory
    {

        public NeuralNetworkRepositoryFactory()
        {
        }

        public INeuralNetworkRepository Get()
        {
            return new NeuralNetworkRepository(
                new JsonFileRepository()
            );
        }       
    }
}
