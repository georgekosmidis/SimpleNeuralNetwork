using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations
{
    public class NetworkLayers : INetworkLayers
    {
        INeuronSynapsis _neuronCompute;

        public NetworkLayers(INeuronSynapsis neuronCompute)
        {
            _neuronCompute = neuronCompute;
        }

        public void Create(NeuralNetwork neuralNetwork, int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {

            for (var i = 0; i < inputNeuronsCount; i++)
                neuralNetwork.InputNeurons.Add(new Neuron());

            for (var j = 0; j < hiddenNeuronsCount; j++)
            {
                var neuron = new Neuron();
                _neuronCompute.Set(neuron, neuralNetwork.InputNeurons);
                neuralNetwork.HiddenNeurons.Add(neuron);
            }

            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var neuron = new Neuron();
                _neuronCompute.Set(neuron, neuralNetwork.HiddenNeurons);
                neuralNetwork.OutputNeurons.Add(neuron);
            }
        }
        
    }
}
