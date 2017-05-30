﻿using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Trainers
{
    public class TrainedNetworksLoader
    {
        NeuralNetworkCompute _neuralNetworkCompute;
        IDataRepository _filehandle;

        public TrainedNetworksLoader(NeuralNetworkCompute neuralNetworkCompute, IDataRepository filehandle)
        {
            _neuralNetworkCompute = neuralNetworkCompute;
            _filehandle = filehandle;
        }

        public void Load(string filename)
        {
            var neuralNetworkModel = _filehandle.Load(filename);
            _neuralNetworkCompute.LoadModel(neuralNetworkModel);
        }

    }
}
