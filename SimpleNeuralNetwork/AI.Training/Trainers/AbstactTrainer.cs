using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Models;
using SimpleNeuralNetwork.EventArgumens;
using SimpleNeuralNetwork.Interfaces;
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
        IDataHandle _filehandle;

        protected virtual NeuralNetworkTrainModel NueralNetworkModel { get; } = new NeuralNetworkTrainModel();

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public AbstactTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle)
        {
            _neuralNetworkCompute = neuralNetworkCompute;
            _filehandle = filehandle;
        }
        public void Save(string filename)
        {
            _filehandle.Save<NeuralNetwork>(filename, _nueralNetwork);
        }
        public void Train()
        {

            _neuralNetworkCompute.CreateLayers(NueralNetworkModel.Count(x => x.Layer == NeuronLayer.Input),
                                               NueralNetworkModel.HiddenNeuronsCount,
                                               NueralNetworkModel.Count(x => x.Layer == NeuronLayer.Output));
            

            var j = 0;
            var leastError = 1d;
            do
            {
                var status = GetMatrixHeaders(j + 1);
                OnUpdateStatus?.Invoke(this, new ProgressEventArgs(status));

                var innerLeastError = 0d;

                for (int i = 0; i < NueralNetworkModel.ValuesCount; i++)
                {
                    _nueralNetwork = _neuralNetworkCompute.Train(NueralNetworkModel.GetValuesForLayer(NeuronLayer.Input, i),
                                                                 NueralNetworkModel.GetValuesForLayer(NeuronLayer.Output, i),
                                                                 NueralNetworkModel.MathFunctions);

                    innerLeastError = Math.Max(innerLeastError, GetMaxError(_nueralNetwork.OutputNeurons));

                    status = GetMatrix(_nueralNetwork.OutputNeurons, NueralNetworkModel.GetValuesForLayer(NeuronLayer.Output, i));
                    OnUpdateStatus?.Invoke(this, new ProgressEventArgs(status));
                }
                leastError = Math.Min(leastError, innerLeastError);

            } while (leastError > NueralNetworkModel.AcceptedError);

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
