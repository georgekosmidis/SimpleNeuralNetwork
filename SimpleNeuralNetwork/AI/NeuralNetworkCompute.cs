using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class NeuralNetworkCompute
    {
        private NeuralNetwork _neuralNetwork;

        private IFeedForward _feedForward;
        private IBackPropagate _backPropagate;
        private INetworkLayers _networkLayers;

        public NeuralNetworkCompute(IFeedForward feedForward, IBackPropagate backPropagate, INetworkLayers networkLayers)
        {
            _neuralNetwork = new NeuralNetwork();
            _feedForward = feedForward;
            _backPropagate = backPropagate;
            _networkLayers = networkLayers;

        }

        public void CreateLayers(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            _networkLayers.Create(_neuralNetwork, inputNeuronsCount, hiddenNeuronsCount, outputNeuronsCount);
        }
        public void LoadModel(NeuralNetwork neuralNetwork)
        {
            _neuralNetwork = neuralNetwork;

            for (var i = 0; i < _neuralNetwork.InputNeurons.Count(); i++)
            {
                for (var j = 0; j < _neuralNetwork.InputNeurons[i].OutputSynapses.Count(); j++)
                {
                    _neuralNetwork.InputNeurons[i].OutputSynapses[j].FromNeuron = _neuralNetwork.InputNeurons[i];
                    _neuralNetwork.InputNeurons[i].OutputSynapses[j].ToNeuron = _neuralNetwork.HiddenNeurons[j];

                    _neuralNetwork.HiddenNeurons[j].InputSynapses[i].FromNeuron = _neuralNetwork.InputNeurons[i];
                    _neuralNetwork.HiddenNeurons[j].InputSynapses[i].ToNeuron = _neuralNetwork.HiddenNeurons[j];
                }
            }

            for (var i = 0; i < _neuralNetwork.HiddenNeurons.Count(); i++)
            {
                for (var j = 0; j < _neuralNetwork.HiddenNeurons[i].OutputSynapses.Count(); j++)
                {
                    _neuralNetwork.HiddenNeurons[i].OutputSynapses[j].FromNeuron = _neuralNetwork.HiddenNeurons[i];
                    _neuralNetwork.HiddenNeurons[i].OutputSynapses[j].ToNeuron = _neuralNetwork.OutputNeurons[j];

                    _neuralNetwork.OutputNeurons[j].InputSynapses[i].FromNeuron = _neuralNetwork.HiddenNeurons[i];
                    _neuralNetwork.OutputNeurons[j].InputSynapses[i].ToNeuron = _neuralNetwork.OutputNeurons[j];
                }
            }
        }


        public double[] Compute(double[] inputData)
        {
            _feedForward.Compute(this._neuralNetwork, inputData);
            return this._neuralNetwork.OutputNeurons.Select(a => a.Value).ToArray();
        }

        public NeuralNetwork Train(double[] inputData, double[] outputData, MathFunctions mathFunctions)
        {
            this._neuralNetwork.MathFunctions = mathFunctions;

            _feedForward.Compute(this._neuralNetwork, inputData);
            _backPropagate.Compute(this._neuralNetwork, outputData);
            return _neuralNetwork;
        }
    }
}
