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

        private List<Neuron> inputNeurons = new List<Neuron>();
        private List<Neuron> hiddenNeurons = new List<Neuron>();
        public List<Neuron> outputNeurons = new List<Neuron>();

        public NeuralNetwork(int inputNeuronsCount, int hiddenNeuronsCount, int outputNeuronsCount)
        {

            var weightsCount = Math.Max(inputNeuronsCount, Math.Max(outputNeuronsCount, hiddenNeuronsCount));

            for (var i = 0; i < inputNeuronsCount; i++)
                this.inputNeurons.Add(new Neuron());

            for (var j = 0; j < hiddenNeuronsCount; j++)
            {
                var n = new Neuron();
                n.SetSynapsis(this.inputNeurons);
                this.hiddenNeurons.Add(n);
            }

            for (int k = 0; k < outputNeuronsCount; k++)
            {
                var n = new Neuron();
                n.SetSynapsis(this.hiddenNeurons);
                this.outputNeurons.Add(n);
            }
        }

        public double[] Compute(double[] inputData)
        {
            FeedForward(inputData);
            return this.outputNeurons.Select(a => a.Value).ToArray();
        }


        public void FeedForward(double[] inputData)
        {

            for (var i = 0; i < inputNeurons.Count(); i++)
                inputNeurons[i].Value = inputData[i];
            //for (var i = 0; i < outputNeurons.Count(); i++)
            //    outputNeurons[i].Output = outputData[i];

            foreach (var hiddenNeuron in hiddenNeurons)
            {
                var total = hiddenNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                hiddenNeuron.Value = Maths.Sigmoid(total);
            }

            foreach (var outputNeuron in outputNeurons)
            {
                var total = outputNeuron.InputSynapses.Sum(x => x.FromNeuron.Value * x.Weight);
                outputNeuron.Value = Maths.Sigmoid(total);
            }
        }

        public void BackPropagate(double[] outputData)
        {
            for (var i = 0; i < outputNeurons.Count(); i++)
                outputNeurons[i].Error = Maths.Derivative(outputNeurons[i].Value) * (outputData[i] - outputNeurons[i].Value);

            foreach (var hiddenNeuron in hiddenNeurons)
                hiddenNeuron.Error = hiddenNeuron.OutputSynapses.Sum(x => x.ToNeuron.Error * x.Weight) * Maths.Derivative(hiddenNeuron.Value);

            foreach (var outputNeuron in outputNeurons)
            {
                outputNeuron.Bias += this.LearningRate * outputNeuron.Error;

                foreach (var synapse in outputNeuron.InputSynapses)
                    synapse.Weight += this.LearningRate * outputNeuron.Error * synapse.FromNeuron.Value;
            }

            foreach (var hiddenNeuron in hiddenNeurons)
            {
                hiddenNeuron.Bias += this.LearningRate * hiddenNeuron.Error;

                foreach (var synapse in hiddenNeuron.InputSynapses)
                    synapse.Weight += this.LearningRate * hiddenNeuron.Error * synapse.FromNeuron.Value;
            }
        }
    }
}
