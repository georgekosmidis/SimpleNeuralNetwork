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
        //int hiddenNeuronsTestCount = 0;
        IMathFactory _mathFactory;

        public NetworkLayers(INeuronSynapsis neuronSynapsis, IMathFactory mathFactory)
        {
            _neuronSynapsis = neuronSynapsis;
            _mathFactory = mathFactory;
        }

        public NeuralNetwork Create(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount, bool autoAdjustHiddenLayer)
        {
            var _mathMethods = _mathFactory.Get(neuralNetwork);

            if (autoAdjustHiddenLayer)
            {
                if (neuralNetwork.HiddenNeurons.Count() <= 0)
                    hiddenNeuronsCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d));
                else
                {
                    hiddenNeuronsCount = neuralNetwork.HiddenNeurons.Count() + 1;
                    //if (hiddenNeuronsTestCount >= 0)
                    //{
                    //    hiddenNeuronsCount++;
                    //    hiddenNeuronsTestCount = 0;
                    //}
                    //hiddenNeuronsTestCount++;
                }
                //neuralNetwork = new NeuralNetwork();
            }
            //else
            neuralNetwork = new NeuralNetwork();


            for (var i = 0; i < inputNeuronsCount; i++)
                neuralNetwork.InputNeurons.Add(new Neuron() { Index = i, Error = 0 });

            if (hiddenNeuronsCount == -1)
                hiddenNeuronsCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d));

            for (var j = 0; j < hiddenNeuronsCount; j++)
            {
                var neuron = new Neuron() { Index = j, Error = _mathMethods.Random() };
                _neuronSynapsis.Set(_mathMethods, neuron, neuralNetwork.InputNeurons);
                neuralNetwork.HiddenNeurons.Add(neuron);
            }

            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var neuron = new Neuron() { Index = k, Error = _mathMethods.Random() };
                _neuronSynapsis.Set(_mathMethods, neuron, neuralNetwork.HiddenNeurons);
                neuralNetwork.OutputNeurons.Add(neuron);
            }

            return neuralNetwork;
        }

    }
}
