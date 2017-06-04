using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Models;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.NeuralNetworkTrainerHelpers
{
    public class ValidationSet : AbstractSet, IValidationSet
    {
        private IOuputDeviation _ouputDeviation;
        private IFeedForward _feedForward;
        double stopIterations_lastOutputDeviation = double.MaxValue;
        //double stopTraining_lastOutputDeviation = double.MaxValue;

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
            //var suffle = Suffle(trainSetCount, validationSetCount);
            //foreach (var i in suffle)
            for (var i = trainSetCount; i < trainSetCount + validationSetCount; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                innerLastOutputDeviation += _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
            }
            innerLastOutputDeviation /= validationSetCount;

            //check deviation to break training
            neuralNetwork.NeuralNetworkError = innerLastOutputDeviation;

            //check to stop cycles with this setup
            if (stopIterations_lastOutputDeviation <= Math.Round(innerLastOutputDeviation, neuralNetwork.Divisor.ToString().Length) ||  //if important digits stopped correcting, stop iterations
                innerLastOutputDeviation < neuralNetworkTrainModel.AcceptedError ||                                                     //if we are in the accepted error range, stop iterations
                Math.Abs(stopIterations_lastOutputDeviation - innerLastOutputDeviation) < 1 / (neuralNetwork.Divisor * 1000))           //if the correction is too small stop iterations
            {
                stopIterations_lastOutputDeviation = double.MaxValue;
                return true;
            }
            stopIterations_lastOutputDeviation = Math.Round(innerLastOutputDeviation, neuralNetwork.Divisor.ToString().Length);

            return false;
        }

        public bool StopTraining(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            //iteration < neuralNetwork.HiddenNeurons.Count() ||
            if (neuralNetworkTrainModel.AutoAdjuctHiddenLayer && neuralNetwork.NeuralNetworkError > neuralNetworkTrainModel.AcceptedError)
            {
                //reconfigure for up to ten times the sum of input/output neurons
                if (neuralNetwork.HiddenNeurons.Count() < (neuralNetwork.InputNeurons.Count() + neuralNetwork.OutputNeurons.Count()) * 10)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
