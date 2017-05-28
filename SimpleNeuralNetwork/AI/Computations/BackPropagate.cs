using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations
{
    public class BackPropagate : IBackPropagate
    {
        IMaths _maths;
        public BackPropagate(IMaths maths)
        {
            _maths = maths;
        }

        public void Compute(NeuralNetwork neuralNetwork, double[] outputData)
        {
            for (var i = 0; i < neuralNetwork.OutputNeurons.Count(); i++)
                neuralNetwork.OutputNeurons[i].Error = _maths.DerivativeMethod(neuralNetwork.OutputNeurons[i].Value) * (outputData[i] - neuralNetwork.OutputNeurons[i].Value);

            foreach (var hiddenNeuron in neuralNetwork.HiddenNeurons)
                hiddenNeuron.Error = hiddenNeuron.OutputSynapses.Sum(x => x.ToNeuron.Error * x.Weight) * _maths.DerivativeMethod(hiddenNeuron.Value);

            foreach (var outputNeuron in neuralNetwork.OutputNeurons)
            {
                outputNeuron.Bias += neuralNetwork.LearningRate * outputNeuron.Error;

                foreach (var synapse in outputNeuron.InputSynapses)
                    synapse.Weight += neuralNetwork.LearningRate * outputNeuron.Error * synapse.FromNeuron.Value;
            }

            foreach (var hiddenNeuron in neuralNetwork.HiddenNeurons)
            {
                hiddenNeuron.Bias += neuralNetwork.LearningRate * hiddenNeuron.Error;

                foreach (var synapse in hiddenNeuron.InputSynapses)
                    synapse.Weight += neuralNetwork.LearningRate * hiddenNeuron.Error * synapse.FromNeuron.Value;
            }
        }
    }
}
