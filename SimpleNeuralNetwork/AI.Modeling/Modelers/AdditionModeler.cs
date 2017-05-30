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
    public class AdditionModeler : IModeler
    {

        public NeuralNetworkTrainModel NeuralNetworkModel { get; } = new NeuralNetworkTrainModel();

        public AdditionModeler()
        {
            NeuralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)
                                        .SetMathFunctions(MathFunctions.Sigmoid)
                                        .SetAcceptedError(.001)
                                        .SetNeuralNetworkName("Addition")

                                        .AddInputNeuron()
                                        .AddValue(.1).AddValue(.3).AddValue(.1).AddValue(.1)
                                        .AddInputNeuron()
                                        .AddValue(.2).AddValue(.1).AddValue(.4).AddValue(.1)

                                        .AddOutputNeuron()
                                        .AddValue(.3).AddValue(.4).AddValue(.5).AddValue(.2)

                                        .Get();
        }
    }
}
