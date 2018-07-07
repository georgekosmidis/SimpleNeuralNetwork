using SimpleNeuralNetwork.Brain.Computations;
using SimpleNeuralNetwork.Brain.Computations.Maths;
using SimpleNeuralNetwork.Brain.Facades;
using SimpleNeuralNetwork.Brain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Factories
{
    public class NeuralNetworkRunnerFactory
    {

        public NeuralNetworkRunnerFactory()
        {
        }

        public INeuralNetworkRunner Get()
        {
            return new NeuralNetworkRunner(
                new FeedForward(
                    new MathFactory()
                )
            );
        }       
    }
}
