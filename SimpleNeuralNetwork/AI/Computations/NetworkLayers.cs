using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
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

        List<int> holdHiddenLayers = new List<int>();
        INeuronSynapsis _neuronSynapsis;
        NeuralNetwork neuralNetwork = new NeuralNetwork();
        //int hiddenNeuronsTestCount = 0;
        IMathFactory _mathFactory;

        public NetworkLayers(INeuronSynapsis neuronSynapsis, IMathFactory mathFactory)
        {
            _neuronSynapsis = neuronSynapsis;
            _mathFactory = mathFactory;
        }

        public NeuralNetwork Create(int inputNeuronsCount, List<HiddenLayerModel> hiddenLayers, int outputNeuronsCount, bool autoAdjustHiddenLayer)
        {
            var _mathMethods = _mathFactory.Get(neuralNetwork);

            if (autoAdjustHiddenLayer)
            {
                var hiddenNeuronsStatingCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d));
                var hiddenNeuronsEndCount = Convert.ToInt32(Math.Ceiling((inputNeuronsCount + outputNeuronsCount) / 2d)) * 2;

                if (neuralNetwork.HiddenLayers.Count() <= 0)
                {
                    holdHiddenLayers.Add(hiddenNeuronsStatingCount);
                }
                else
                {
                    var lastHiddenLayer = holdHiddenLayers.Count() - 1;
                    if (holdHiddenLayers[lastHiddenLayer] >= hiddenNeuronsEndCount)
                    {
                        holdHiddenLayers[lastHiddenLayer] = hiddenNeuronsStatingCount;
                        holdHiddenLayers.Add(hiddenNeuronsStatingCount);
                    }
                    else
                    {
                        holdHiddenLayers[lastHiddenLayer]++;
                    }

                    //hiddenNeuronsCount = neuralNetwork.HiddenNeurons.Count() + 1;
                    //if (hiddenNeuronsTestCount >= 0)
                    //{
                    //    hiddenNeuronsCount++;
                    //    hiddenNeuronsTestCount = 0;
                    //}
                    //hiddenNeuronsTestCount++;
                }
            }
            else
            {
                for (var i = 0; i < hiddenLayers.Count(); i++)
                    holdHiddenLayers.Add(hiddenLayers[i].NeuronsCount);
            }
            neuralNetwork = new NeuralNetwork();

            var neuronsIndex = 0;

            for (var i = 0; i < inputNeuronsCount; i++)
                neuralNetwork.InputNeurons.Add(new Neuron() { Index = neuronsIndex++, Error = 0, Layer = 0 });


            //hidden layers
            for (var i = 0; i < holdHiddenLayers.Count(); i++)
            {
                neuralNetwork.HiddenLayers.Add(new List<Neuron>());
                for (var j = 0; j < holdHiddenLayers[i]; j++)
                {
                    var neuron = new Neuron() { Index = neuronsIndex++, Error = _mathMethods.Random(), Layer = i + 1 };
                    if (i == 0)
                        _neuronSynapsis.Set(_mathMethods, neuron, neuralNetwork.InputNeurons);
                    else
                        _neuronSynapsis.Set(_mathMethods, neuron, neuralNetwork.HiddenLayers[i - 1]);

                    neuralNetwork.HiddenLayers[i].Add(neuron);
                }
            }

            //from last hidden layer to output
            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var neuron = new Neuron() { Index = neuronsIndex++, Error = _mathMethods.Random(), Layer = holdHiddenLayers.Count() + 1 };
                _neuronSynapsis.Set(_mathMethods, neuron, neuralNetwork.HiddenLayers.Last());
                neuralNetwork.OutputNeurons.Add(neuron);
            }

            return neuralNetwork;
        }

    }
}
