using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
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

        public void LoadModel(NeuralNetwork neuralNetwork)
        {
            _neuralNetwork = neuralNetwork;
        }

        public void CreateLayers(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {
            _networkLayers.Create(_neuralNetwork, inputNeuronsCount, hiddenNeuronsCount, outputNeuronsCount);
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
