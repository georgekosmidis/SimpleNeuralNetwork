using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training
{
    public class ValidationSet : IValidationSet
    {
        private IOuputDeviation _ouputDeviation;
        private IFeedForward _feedForward;
        double stopIterations_lastOutputDeviation = double.MaxValue;
        double stopTraining_lastOutputDeviation = double.MaxValue;

        public ValidationSet(IFeedForward feedForward, IOuputDeviation ouputDeviation)
        {
            _ouputDeviation = ouputDeviation;
            _feedForward = feedForward;
        }

        public bool StopIterations(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .7));
            var validationSetCount = Convert.ToInt32(Math.Floor((neuralNetworkTrainModel.ValuesCount - trainSetCount) * .7));

            var innerLastOutputDeviation = 0d;
            for (var i = trainSetCount; i < trainSetCount + validationSetCount; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                innerLastOutputDeviation += _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
            }

            //check deviation to break training
            innerLastOutputDeviation /= validationSetCount;
            stopTraining_lastOutputDeviation = innerLastOutputDeviation;
            if (stopIterations_lastOutputDeviation <= innerLastOutputDeviation ||                      //if new outputDeviation is bigger, stop training
                innerLastOutputDeviation < neuralNetworkTrainModel.AcceptedError ||                    //if we are in the accepted error range, stop training
                stopIterations_lastOutputDeviation - innerLastOutputDeviation < .00001)                //if the correction is too small stop training
            {
                stopIterations_lastOutputDeviation = double.MaxValue;
                neuralNetwork.NeuralNetworkError = innerLastOutputDeviation;
                return true;
            }
            stopIterations_lastOutputDeviation = innerLastOutputDeviation;
            neuralNetwork.NeuralNetworkError = innerLastOutputDeviation;
            return false;
        }

        public bool StopTraining(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            //iteration < neuralNetwork.HiddenNeurons.Count() ||
            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && stopTraining_lastOutputDeviation > neuralNetworkTrainModel.AcceptedError)
            {
                //reconfigure for up to ten times the sum of input/output neurons
                if (neuralNetwork.HiddenNeurons.Count() < (neuralNetwork.InputNeurons.Count() + neuralNetwork.OutputNeurons.Count()) * 10)
                {
                    return false;
                    //neuralNetwork = Train(neuralNetworkTrainModel);
                }
            }
            return true;
        }
    }
}
