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
    public class TestSet : ITestSet
    {
        private IOuputDeviation _ouputDeviation;
        private IFeedForward _feedForward;

        public TestSet(IFeedForward feedForward, IOuputDeviation ouputDeviation)
        {
            _ouputDeviation = ouputDeviation;
            _feedForward = feedForward;
        }

        public void Test(NeuralNetwork neuralNetwork, ProblemDescriptionModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .66));
            var validationSetCount = Convert.ToInt32(Math.Floor((neuralNetworkTrainModel.ValuesCount - trainSetCount) * .66));
            var testSet = Convert.ToInt32(neuralNetworkTrainModel.ValuesCount - trainSetCount - validationSetCount);

            var testError = 0d;
            for (var i = trainSetCount + validationSetCount; i < trainSetCount + validationSetCount + testSet; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetInputValues(i));
                testError = Math.Max(testError, _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetOutputValues(i)));
            }
            neuralNetwork.NeuralNetworkError = testError;// / testSet;//update with test error
        }
    }
}
