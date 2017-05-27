using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class NeuralNetwork
    {
        /// Represents the learging rate used in gradient descent to prevent weights from converging at sub-optimal solutions.
        public double LearningRate = 0.7;

        public List<Neuron> InputNeurons { get; private set; } = new List<Neuron>();
        public List<Neuron> HiddenNeurons { get; private set; } = new List<Neuron>();
        public List<Neuron> OutputNeurons { get; private set; } = new List<Neuron>();

        public NeuralNetwork()
        {

        }

        public void CreateLayers(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {

            for (var i = 0; i < inputNeuronsCount; i++)
                this.InputNeurons.Add(new Neuron());

            for (var j = 0; j < hiddenNeuronsCount; j++)
            {
                var n = new Neuron();
                n.SetSynapsis(this.InputNeurons);
                this.HiddenNeurons.Add(n);
            }

            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var n = new Neuron();
                n.SetSynapsis(this.HiddenNeurons);
                this.OutputNeurons.Add(n);
            }
        }

        public double[] Compute(double[] inputData)
        {
            FeedForward(inputData);
            return this.OutputNeurons.Select(a => a.Value).ToArray();
        }


        public void FeedForward(double[] inputData)
        {
            
            for (var i = 0; i < InputNeurons.Count(); i++)
                InputNeurons[i].Value = inputData[i];

            foreach (var hiddenNeuron in HiddenNeurons)
            {
                var total = hiddenNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                hiddenNeuron.Value = Maths.Sigmoid(total);
            }

            foreach (var outputNeuron in OutputNeurons)
            {
                var total = outputNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                outputNeuron.Value = Maths.Sigmoid(total);
            }
        }

        public void BackPropagate(double[] outputData)
        {
            for (var i = 0; i < OutputNeurons.Count(); i++)
                OutputNeurons[i].Error = Maths.Derivative(OutputNeurons[i].Value) * (outputData[i] - OutputNeurons[i].Value);

            foreach (var hiddenNeuron in HiddenNeurons)
                hiddenNeuron.Error = hiddenNeuron.OutputSynapses.Sum(x => x.ToNeuron.Error * x.Weight) * Maths.Derivative(hiddenNeuron.Value);

            foreach (var outputNeuron in OutputNeurons)
            {
                outputNeuron.Bias += this.LearningRate * outputNeuron.Error;

                foreach (var synapse in outputNeuron.InputSynapses)
                    synapse.Weight += this.LearningRate * outputNeuron.Error * synapse.FromNeuron.Value;
            }

            foreach (var hiddenNeuron in HiddenNeurons)
            {
                hiddenNeuron.Bias += this.LearningRate * hiddenNeuron.Error;

                foreach (var synapse in hiddenNeuron.InputSynapses)
                    synapse.Weight += this.LearningRate * hiddenNeuron.Error * synapse.FromNeuron.Value;
            }
        }
    }
}
