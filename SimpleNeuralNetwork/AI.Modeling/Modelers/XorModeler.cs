using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers
{
    public class XorModeler : IModeler
    {
        public NeuralNetworkTrainModel NeuralNetworkModel { get; } = new NeuralNetworkTrainModel();


        public XorModeler()
        {
            NeuralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)
                                        .SetMathFunctions(MathFunctions.Sigmoid)
                                        .SetAcceptedError(.02)
                                        .SetNeuralNetworkName("XOR")

                                        .AddInputNeuron(x => x.AddValues(0, 1, 0, 1))
                                        .AddInputNeuron(x => x.AddValues(0, 0, 1, 1))
                                        .AddOutputNeuron(x => x.AddValues(0, 1, 1, 0))

                                        .Get();
        }
    }
}
