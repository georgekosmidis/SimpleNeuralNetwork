using SimpleNeuralNetwork.Brain.Trainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;

namespace SimpleNeuralNetwork.Brain.Trainer.NeuralNetworkTrainerHelpers
{
    public class TrainSet : AbstractSet, ITrainSet
    {
        private IBackPropagate _backPropagate;
        private IFeedForward _feedForward;

        public TrainSet(IFeedForward feedForward, IBackPropagate backPropagate)
        {
            _backPropagate = backPropagate;
            _feedForward = feedForward;
        }

        public void Train(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .66));

            if (trainSetCount < neuralNetwork.InputNeurons.Count() + neuralNetwork.HiddenLayers.Sum(x => x.Count()) + neuralNetwork.OutputNeurons.Count())
                trainSetCount = neuralNetworkTrainModel.ValuesCount;


            //var suffle = Suffle(0, trainSetCount);
            //foreach (var i in suffle)
            for (var i = 0; i < trainSetCount; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetInputValues(i));
                _backPropagate.Compute(neuralNetwork, neuralNetworkTrainModel.GetOutputValues(i));
            }
        }


    }
}
