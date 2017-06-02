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
        INeuronSynapsis _neuronSynapsis;
        NeuralNetwork neuralNetwork = new NeuralNetwork();
        int hiddenNeuronsTestCount = 0;

        public NetworkLayers(INeuronSynapsis neuronSynapsis)
        {
            _neuronSynapsis = neuronSynapsis;
        }

        public NeuralNetwork Create(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, bool autoAdjustHiddenLayer)
        {

            if (autoAdjustHiddenLayer)
            {
                if (neuralNetwork.HiddenNeurons.Count() <= 0)
                    hiddenNeuronsCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d));
                else
                {
                    hiddenNeuronsCount = neuralNetwork.HiddenNeurons.Count();
                    if (hiddenNeuronsTestCount >= 5)
                    {
                        hiddenNeuronsCount++;
                        hiddenNeuronsTestCount = 0;
                    }
                    hiddenNeuronsTestCount++;
                }
                neuralNetwork = new NeuralNetwork();
            }
            else
                neuralNetwork = new NeuralNetwork();


            for (var i = 0; i < inputNeuronsCount; i++)
                neuralNetwork.InputNeurons.Add(new Neuron() { Index = i });

            if (hiddenNeuronsCount == -1)
                hiddenNeuronsCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d));

            for (var j = 0; j < hiddenNeuronsCount; j++)
            {
                var neuron = new Neuron() { Index = j };
                _neuronSynapsis.Set(neuron, neuralNetwork.InputNeurons);
                neuralNetwork.HiddenNeurons.Add(neuron);
            }

            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var neuron = new Neuron() { Index = k };
                _neuronSynapsis.Set(neuron, neuralNetwork.HiddenNeurons);
                neuralNetwork.OutputNeurons.Add(neuron);
            }

            return neuralNetwork;
        }

    }
}
