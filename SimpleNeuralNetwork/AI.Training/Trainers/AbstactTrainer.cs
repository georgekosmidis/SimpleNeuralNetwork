﻿using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.EventArguments;
using SimpleNeuralNetwork.AI.Training.Interfaces;
using SimpleNeuralNetwork.AI.Training.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Trainers
{
    public abstract class AbstactTrainer
    {
        NeuralNetworkCompute _neuralNetworkCompute;
        NeuralNetwork _nueralNetwork = new NeuralNetwork();
        IDataRepository _dataRepository;

        protected virtual NeuralNetworkTrainModel NeuralNetworkModel { get; } = new NeuralNetworkTrainModel();

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public AbstactTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataRepository dataRepository)
        {
            _neuralNetworkCompute = neuralNetworkCompute;
            _dataRepository = dataRepository;
        }
        public void Save(string filename)
        {
            _dataRepository.Save(filename, _nueralNetwork);
        }
        public void Train()
        {

            _neuralNetworkCompute.CreateLayers(NeuralNetworkModel.Count(x => x.Layer == NeuronLayer.Input),
                                               NeuralNetworkModel.HiddenNeuronsCount,
                                               NeuralNetworkModel.Count(x => x.Layer == NeuronLayer.Output));
            

            var j = 0;
            var leastError = 1d;
            do
            {
                var status = GetMatrixHeaders(j + 1);
                OnUpdateStatus?.Invoke(this, new ProgressEventArgs(status));

                var innerLeastError = 0d;

                for (int i = 0; i < NeuralNetworkModel.ValuesCount; i++)
                {
                    _nueralNetwork = _neuralNetworkCompute.Train(NeuralNetworkModel.GetValuesForLayer(NeuronLayer.Input, i),
                                                                 NeuralNetworkModel.GetValuesForLayer(NeuronLayer.Output, i),
                                                                 NeuralNetworkModel.MathFunctions);

                    innerLeastError = Math.Max(innerLeastError, GetMaxError(_nueralNetwork.OutputNeurons));

                    status = GetMatrix(_nueralNetwork.OutputNeurons, NeuralNetworkModel.GetValuesForLayer(NeuronLayer.Output, i));
                    OnUpdateStatus?.Invoke(this, new ProgressEventArgs(status));
                }
                leastError = Math.Min(leastError, innerLeastError);

            } while (leastError > NeuralNetworkModel.AcceptedError);

        }


        private double GetMaxError(List<Neuron> neurons)
        {
            var maxError = 0d;
            foreach (var neuron in neurons)
            {
                maxError = Math.Max(maxError, Math.Abs(neuron.Error));
            }
            return maxError;
        }

        private string GetMatrixHeaders(int iteration)
        {
            var s = new StringBuilder();
            s.AppendLine(new String('*', 50));
            s.AppendLine("Starting Iteration " + iteration);
            s.AppendLine(new String('-', 50));
            s.AppendLine(String.Format("{0,10} |{1,10} |{2,10} |{3,10}", "Neuron", "Expected", "Actual", "Error"));
            return s.ToString();
        }
        private string GetMatrix(List<Neuron> neurons, double[] expectedVaues)
        {
            var s = new StringBuilder();
            var i = 0;
            foreach (var neuron in neurons)
            {
                s.Append(
                String.Format("{0,10} |{1,10} |{2,10} |{3,10}",
                               (i + 1),
                               expectedVaues[i++].ToString("0.0000", CultureInfo.InvariantCulture),
                               neuron.Value.ToString("0.0000", CultureInfo.InvariantCulture),
                               neuron.Error.ToString("0.0000", CultureInfo.InvariantCulture)
                            )
                );
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
