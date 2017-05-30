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
            //Create samples
            // 3 Input neurons, 2 output neurons
            // First output neuron value is the sum of inputs, second output neuron the difference
            var samples = 1000;
            var input1 = new double[samples];
            var input2 = new double[samples];
            var input3 = new double[samples];
            var output1 = new double[samples];
            var output2 = new double[samples];
            var rnd = new Random(1);//same samples each time
            for (var i = 0; i < samples; i++)
            {
                input1[i] = Math.Round(rnd.NextDouble() / 3.33, 3);
                input2[i] = Math.Round(rnd.NextDouble() / 3.33, 3);
                input3[i] = Math.Round(rnd.NextDouble() / 3.34, 3);
                output1[i] = input1[i] + input2[i] + input3[i];
                output2[i] = input1[i] - input2[i] - input3[i];
            }
            NeuralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)
                                        .SetMathFunctions(MathFunctions.HyperTan)
                                        .SetAcceptedError(.001)
                                        .SetNeuralNetworkName("Addition")

                                        .AddInputNeuron(x => x.AddValues(input1))
                                        .AddInputNeuron(x => x.AddValues(input2))
                                        .AddInputNeuron(x => x.AddValues(input3))

                                        .AddOutputNeuron(x => x.AddValues(output1))
                                        .AddOutputNeuron(x => x.AddValues(output2))

                                        .Get();
        }
    }
}
