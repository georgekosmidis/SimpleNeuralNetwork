using SimpleNeuralNetwork.Brain.Computations;
using SimpleNeuralNetwork.Brain.Computations.Maths;
using SimpleNeuralNetwork.Brain.Trainer.Facades;
using SimpleNeuralNetwork.Brain.Trainer.Interfaces;
using SimpleNeuralNetwork.Brain.Trainer.NeuralNetworkTrainerHelpers;
using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Modeler.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Factories
{
    public class ProblemDescriptorFactory
    {
        private Dictionary<Type, IProblem> cache = new Dictionary<Type, IProblem>();

        public ProblemDescriptorFactory()
        {
            //TODO: search with reflection
            cache.Add(typeof(IProblemLotto), new Lotto());
            cache.Add(typeof(IProblemAddSubtract), new AddSubtract());
            cache.Add(typeof(IProblemCustom), new Custom());

        }

        public IProblem Get<T>() where T: class
        {
            return cache.First(x => x.Key == typeof(T)).Value as IProblem;
        }
    }
}
