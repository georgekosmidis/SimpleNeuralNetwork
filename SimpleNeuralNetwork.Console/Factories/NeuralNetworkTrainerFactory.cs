using SimpleNeuralNetwork.Brain.Computations;
using SimpleNeuralNetwork.Brain.Computations.Maths;
using SimpleNeuralNetwork.Brain.Trainer.Facades;
using SimpleNeuralNetwork.Brain.Trainer.Interfaces;
using SimpleNeuralNetwork.Brain.Trainer.NeuralNetworkTrainerHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Factories
{
    public class NeuralNetworkTrainerFactory
    {

        public NeuralNetworkTrainerFactory()
        {
        }

        public INeuralNetworkTrainer Get()
        {
            return new NeuralNetworkTrainer(
                new NetworkLayers(
                    new NeuronSynapsis(),
                    new MathFactory()
                ),
                new TrainSet(
                    new FeedForward(
                        new MathFactory()
                    ),
                    new BackPropagate(
                        new MathFactory()
                    )
                ),
                new ValidationSet(
                    new FeedForward(
                        new MathFactory()
                    ),
                    new OuputDeviation()
                ),
                new TestSet(
                    new FeedForward(
                        new MathFactory()
                    ),
                    new OuputDeviation()
                )
            );
        }       
    }
}
