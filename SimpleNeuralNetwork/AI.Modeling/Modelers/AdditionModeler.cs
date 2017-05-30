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

                                        .AddInputNeuron(x => x.AddValues(.1, .3, .1, .1))
                                        .AddInputNeuron(x => x.AddValues(.2, .1, .4, .1))

                                        .AddOutputNeuron(x => x.AddValues(.3, .4, .5, .2))

                                        .Get();
        }
    }
}
