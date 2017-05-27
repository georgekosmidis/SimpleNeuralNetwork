using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class AdditionTrainer : ITrainer
    {
        private NeuralNetwork _neuralNetwork;
        private IFileHandle _filehandle;

        //Input data, defines variables inserted into the system, second dimension defines number of input neurons, for this case 2
        private double[][] inputData = new double[][] {
                                new double[] { .1, .2 },
                                new double[] { .3, .1 },
                                new double[] { .1, .4 },
                                new double[] { .1, .1 }
                            };

        //Expected output data defines expected result, second dimension defines number of output neurons, for this case 1
        private double[][] resultsData = new double[][] {
                                   new double[] { .3 },
                                   new double[] { .4 },
                                   new double[] { .5 },
                                   new double[] { .2 },
                               };

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public AdditionTrainer(NeuralNetwork neuralNetwork, IFileHandle filehandle)
        {
            _neuralNetwork = neuralNetwork;
            _filehandle = filehandle;
        }
        public void Save(string filename)
        {
            _filehandle.Write<NeuralNetwork>(filename, _neuralNetwork);
        }
        public void Train(int hiddenLayerNeurons, double acceptedError)
        {
            //Create a neuron network with 2 input neurons, 5 hidden neurons and 1 output neuron
            _neuralNetwork.CreateLayers(inputData[0].Length, hiddenLayerNeurons, resultsData[0].Length);
            //Train
            var j = 0;
            var leastError = 1d;
            do//could be smaller but training data are few and makes no point...
            {
                OnUpdateStatus?.Invoke(this, new ProgressEventArgs("Iteration : " + (j++)));
                OnUpdateStatus?.Invoke(this, new ProgressEventArgs("---------------------"));

                var innerLeastError = 0d;
                for (int i = 0; i < inputData.Length; i++)
                {
                    _neuralNetwork.FeedForward(inputData[i]);
                    _neuralNetwork.BackPropagate(resultsData[i]);

                    var status = "Output : " + _neuralNetwork.OutputNeurons[0].Value.ToString("0.000000") + " " +
                                 "Expected : " + resultsData[i][0].ToString("0.000000") + " " +
                                 "Error : " + _neuralNetwork.OutputNeurons[0].Error.ToString("0.000000") + " ";
                    //ouput has only one neuron
                    OnUpdateStatus?.Invoke(this, new ProgressEventArgs(status));

                    innerLeastError = Math.Max(innerLeastError, Math.Abs(_neuralNetwork.OutputNeurons[0].Error));
                }
                leastError = Math.Min(leastError, innerLeastError);

                OnUpdateStatus?.Invoke(this, new ProgressEventArgs("---------------------"));
            } while (leastError > acceptedError);


            OnUpdateStatus?.Invoke(this, new ProgressEventArgs("Done after " + j + " iterations..."));

        }
    }
}
