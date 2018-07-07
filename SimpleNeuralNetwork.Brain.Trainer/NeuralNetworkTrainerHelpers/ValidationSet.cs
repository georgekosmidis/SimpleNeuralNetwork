using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using SimpleNeuralNetwork.Brain.Trainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Trainer.NeuralNetworkTrainerHelpers
{
    public class ValidationSet : AbstractSet, IValidationSet
    {
        private IOuputDeviation _ouputDeviation;
        private IFeedForward _feedForward;
        double stopIterations_lasMaxtOutputDeviation = double.MaxValue;
        //double stopTraining_lastOutputDeviation = double.MaxValue;

        public ValidationSet(IFeedForward feedForward, IOuputDeviation ouputDeviation)
        {
            _ouputDeviation = ouputDeviation;
            _feedForward = feedForward;
        }

        public bool StopIterations(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .66));
            var validationSetCount = Convert.ToInt32(Math.Floor((neuralNetworkTrainModel.ValuesCount - trainSetCount) * .66));

            var innerLastOutputDeviation = 0d;
            for (var i = trainSetCount; i < trainSetCount + validationSetCount; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetInputValues(i));
                innerLastOutputDeviation = Math.Max(innerLastOutputDeviation, _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetOutputValues(i)));
            }
            //innerLastOutputDeviation /= validationSetCount;

            //check deviation to break training
            neuralNetwork.NeuralNetworkError = innerLastOutputDeviation;

            //check to stop cycles with this setup
            if (Math.Round(stopIterations_lasMaxtOutputDeviation, neuralNetwork.Divisor.ToString().Length)
                           <= Math.Round(innerLastOutputDeviation, neuralNetwork.Divisor.ToString().Length) ||                                      //if important digits stopped correcting, stop iterations
                innerLastOutputDeviation < neuralNetworkTrainModel.AcceptedError ||                                                                 //if we are in the accepted error range, stop iterations
                Math.Abs(Math.Abs(stopIterations_lasMaxtOutputDeviation) - Math.Abs(innerLastOutputDeviation)) < 1 / (neuralNetwork.Divisor * 1000))   //if the correction is too small stop iterations
            {
                stopIterations_lasMaxtOutputDeviation = double.MaxValue;
                return true;
            }
            stopIterations_lasMaxtOutputDeviation = innerLastOutputDeviation;

            return false;
        }

        public bool StopTraining(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel)
        {
            //iteration < neuralNetwork.HiddenNeurons.Count() ||
            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetwork.NeuralNetworkError > neuralNetworkTrainModel.AcceptedError)
            {
                //reconfigure for up to ten times the sum of input/output neurons
                foreach (var layer in neuralNetwork.HiddenLayers)
                {
                    if (layer.Count() >= (neuralNetwork.InputNeurons.Count() + neuralNetwork.OutputNeurons.Count()) * 10)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
    }
}
