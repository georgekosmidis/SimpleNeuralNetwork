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
                                        .SetAcceptedError(.001)
                                        .SetNeuralNetworkName("XOR")

                                        .AddInputNeuron()
                                        .AddValue(0).AddValue(1).AddValue(0).AddValue(1)
                                        .AddInputNeuron()
                                        .AddValue(0).AddValue(0).AddValue(1).AddValue(1)

                                        .AddOutputNeuron()
                                        .AddValue(0).AddValue(1).AddValue(1).AddValue(0)

                                        .Get();
        }
    }
}
