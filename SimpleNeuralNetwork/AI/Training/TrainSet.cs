﻿using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training
{
    public class TrainSet : ITrainSet
    {
        private IBackPropagate _backPropagate;
        private IFeedForward _feedForward;

        public TrainSet(IFeedForward feedForward, IBackPropagate backPropagate)
        {
            _backPropagate = backPropagate;
            _feedForward = feedForward;
        }

        public void Train(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .7));

            for (var i = 0; i < trainSetCount; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                _backPropagate.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
            }
        }
    }
}