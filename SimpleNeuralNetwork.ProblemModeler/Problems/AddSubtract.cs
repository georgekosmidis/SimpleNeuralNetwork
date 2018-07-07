using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.ProblemModeler.Creators;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Modeler.Problems
{
    public class AddSubtract : IProblem, IProblemAddSubtract
    {

        public ProblemDescriptionModel Get()
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
            var rnd = new Random(1);//same samples each time for testing
            for (var i = 0; i < samples; i++)
            {
                input1[i] = rnd.Next();
                input2[i] = rnd.Next();
                input3[i] = rnd.Next();
                output1[i] = input1[i] + input2[i] + input3[i];
                output2[i] = input1[i] - input2[i] - input3[i];
            }
           return new ProblemDescriptionCreator()

                    .AutoAdjustHiddenLayer()
                    //.SetMathFunctions(MathFunctions.HyperTan)
                    //.AddHiddenLayer(x => x.AddHiddenLayer(5).AddHiddenLayer(5))
                    .SetAcceptedError(.02)
                    .SetNeuralNetworkName("AddSubtract")

                    .AddInputNeuron(x => x.AddValues(input1))
                    .AddInputNeuron(x => x.AddValues(input2))
                    .AddInputNeuron(x => x.AddValues(input3))

                    .AddOutputNeuron(x => x.AddValues(output1))
                    .AddOutputNeuron(x => x.AddValues(output2))

                    .Get();
        }

    }
}
