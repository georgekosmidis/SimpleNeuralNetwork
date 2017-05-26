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

        private double[][] inputData;
        private double[][] outputData;

        private Neurons inputNeurons;
        private Neurons hiddenNeurons;
        private Neurons outputNeurons;

        public NeuralNetwork(double[][] inputData, double[][] outputData, int hiddenNeurons)
        {

            this.inputData = inputData;
            this.outputData = outputData;

            var weightsCount = (this.inputData[0].Length * this.outputData[0].Length * hiddenNeurons);

            inputNeurons = new Neurons();
            for (var i = 0; i < this.inputData[0].Length; i++)
            {
                var a = new Neuron(0, i);
                this.inputNeurons.Add(a);
            }
            this.inputNeurons.SetWeights(weightsCount);

            this.hiddenNeurons = new Neurons();
            for (var j = 0; j < hiddenNeurons; j++)
            {
                var b = new Neuron(1, j);
                this.hiddenNeurons.Add(b);
            }
            this.hiddenNeurons.SetWeights(weightsCount);

            outputNeurons = new Neurons();
            for (int k = 0; k < this.outputData[0].Length; k++)
            {
                var c = new Neuron(2, k);
                this.outputNeurons.Add(c);
            }
            this.outputNeurons.SetWeights(weightsCount);
        }


        public Neurons Guess(double[] input)
        {

            //feed input neurons
            for (var i = 0; i < inputNeurons.Count(); i++)
                inputNeurons[i].Input = input[i];


            //feedforward from input to hidden Neurons
            for (var i = 0; i < hiddenNeurons.Count(); i++)
            {
                double inputNeuronsTotal = 0.0;
                for (var j = 0; j < inputNeurons.Count(); j++)
                    inputNeuronsTotal += inputNeurons[j].Output * inputNeurons[j].Weight[i];//inputNeurons[j].Weight[j];

                hiddenNeurons[i].Input = inputNeuronsTotal + hiddenNeurons[i].Bias;
            }

            //feedforward from hidden to output Neurons            
            for (var i = 0; i < outputNeurons.Count(); i++)
            {
                double hiddenNeuronsTotal = 0.0;
                for (var j = 0; j < hiddenNeurons.Count(); j++)
                    hiddenNeuronsTotal += hiddenNeurons[j].Output * outputNeurons[i].Weight[j];//outputNeurons[i].Weight[j];

                outputNeurons[i].Input = hiddenNeuronsTotal + outputNeurons[i].Bias;
            }

            return outputNeurons;
        }

        public void FeedForward(int sampleNubmer)
        {
            //var cycles = 0;
            //while (cycles++ <= learningCycles)
            //{
            for (var i = 0; i < inputNeurons.Count(); i++)
                inputNeurons[i].Input = this.inputData[sampleNubmer][i];

            //feedforward from hidden to output Neurons            
            for (var i = 0; i < outputNeurons.Count(); i++)
            {
                outputNeurons[i].OutputTraining = this.outputData[sampleNubmer][i];

                double hiddenNeuronsTotal = 0.0;
                for (var j = 0; j < hiddenNeurons.Count(); j++)
                    hiddenNeuronsTotal += hiddenNeurons[j].Output * outputNeurons[i].Weight[j];//outputNeurons[i]

                outputNeurons[i].Input = hiddenNeuronsTotal + outputNeurons[i].Bias;
            }

            //feedforward from input to hidden Neurons
            for (var i = 0; i < hiddenNeurons.Count(); i++)
            {
                double inputNeuronsTotal = 0.0;
                for (var j = 0; j < inputNeurons.Count(); j++)
                    inputNeuronsTotal += inputNeurons[j].Output * inputNeurons[j].Weight[j];

                hiddenNeurons[i].Input = inputNeuronsTotal + hiddenNeurons[i].Bias;
            }



        }

        public void BackPropagate()
        {

            //calculate error rate for Output layer
            for (var i = 0; i < outputNeurons.Count(); i++)
                outputNeurons[i].Error = outputNeurons[i].SigmoidDerivative(outputNeurons[i].Output) * (outputNeurons[i].OutputTraining - outputNeurons[i].Output);

            //error from output to hidden layer
            for (var i = 0; i < hiddenNeurons.Count(); i++)
            {
                double outputNeuronsTotal = 0.0;
                for (var j = 0; j < outputNeurons.Count(); j++)
                    outputNeuronsTotal += outputNeurons[j].Error * outputNeurons[j].Weight[i];

                hiddenNeurons[i].Error = hiddenNeurons[i].Sigmoid(outputNeurons[0].Output) * outputNeuronsTotal;
            }

            //update all weights from output to hidden
            for (var i = 0; i < outputNeurons.Count(); i++)
            {
                for (var j = 0; j < hiddenNeurons.Count(); j++)
                    hiddenNeurons[j].Weight[i] += this.LearningRate * outputNeurons[i].Error * outputNeurons[i].Output;

                outputNeurons[i].Bias += this.LearningRate * outputNeurons[i].Error;

            }

            //update all weights from hidden to input
            for (var i = 0; i < hiddenNeurons.Count(); i++)
            {
                for (var j = 0; j < inputNeurons.Count(); j++)
                    inputNeurons[j].Weight[i] += this.LearningRate * hiddenNeurons[i].Error * inputNeurons[j].Output;

                hiddenNeurons[i].Bias += this.LearningRate * hiddenNeurons[i].Error;

            }

        }
    }
}
