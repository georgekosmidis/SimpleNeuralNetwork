using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class TrainDataLoader
    {
        NeuralNetworkCompute _neuralNetworkCompute;
        IDataHandle _filehandle;

        public TrainDataLoader(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle)
        {
            _neuralNetworkCompute = neuralNetworkCompute;
            _filehandle = filehandle;
        }

        public void Load(string filename)
        {
            var neuralNetworkModel = _filehandle.Load<NeuralNetwork>(filename);
            _neuralNetworkCompute.LoadModel(neuralNetworkModel);
        }

    }
}
