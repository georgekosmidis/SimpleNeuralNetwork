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
        IMathFactory _mathFactory;
        public BackPropagate(IMathFactory mathFactory)
        {
            _mathFactory = mathFactory;
        }

        public void Compute(NeuralNetwork neuralNetwork, double[] outputData)
        {
            neuralNetwork.HiddenLayers.Reverse();

            var _mathMethods = _mathFactory.Get(neuralNetwork);

            //calculate ouput error
            for (var i = 0; i < neuralNetwork.OutputNeurons.Count(); i++)
                neuralNetwork.OutputNeurons.ElementAt(i).Error = _mathMethods.DerivativeMethod(neuralNetwork.OutputNeurons.ElementAt(i).Value) * (outputData[i] - neuralNetwork.OutputNeurons.ElementAt(i).Value);


            //propagate error to hidden neurons
            
            foreach (var hiddenLayer in neuralNetwork.HiddenLayers)
                foreach (var hiddenNeuron in hiddenLayer)
                    hiddenNeuron.Error = hiddenNeuron.OutputSynapses.Sum(x => x.ToNeuron.Error * x.Weight) * _mathMethods.DerivativeMethod(hiddenNeuron.Value);


            //calculate weight of hidden->output synapsis
            foreach (var outputNeuron in neuralNetwork.OutputNeurons)
            {
                foreach (var synapse in outputNeuron.InputSynapses)
                    synapse.Weight += neuralNetwork.LearningRate * outputNeuron.Error * synapse.FromNeuron.Value;

            }


            //caclulcate weight of input->hidden synapsis
            foreach (var hiddenLayer in neuralNetwork.HiddenLayers)
                foreach (var hiddenNeuron in hiddenLayer)
                {
                    foreach (var synapse in hiddenNeuron.InputSynapses)
                        synapse.Weight += neuralNetwork.LearningRate * hiddenNeuron.Error * synapse.FromNeuron.Value;

                }

            neuralNetwork.HiddenLayers.Reverse();
        }
    }
}
