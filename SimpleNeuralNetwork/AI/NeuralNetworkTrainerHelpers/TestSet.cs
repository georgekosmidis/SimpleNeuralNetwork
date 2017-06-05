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
    public class TestSet : ITestSet
    {
        private IOuputDeviation _ouputDeviation;
        private IFeedForward _feedForward;

        public TestSet(IFeedForward feedForward, IOuputDeviation ouputDeviation)
        {
            _ouputDeviation = ouputDeviation;
            _feedForward = feedForward;
        }

        public void Test(NeuralNetwork neuralNetwork, NeuralNetworkTrainModel neuralNetworkTrainModel)
        {
            var trainSetCount = Convert.ToInt32(Math.Floor(neuralNetworkTrainModel.ValuesCount * .7));
            var validationSetCount = Convert.ToInt32(Math.Floor((neuralNetworkTrainModel.ValuesCount - trainSetCount) * .7));
            var testSet = Convert.ToInt32(neuralNetworkTrainModel.ValuesCount - trainSetCount - validationSetCount);

            var testError = 0d;
            for (var i = trainSetCount + validationSetCount; i < trainSetCount + validationSetCount + testSet; i++)
            {
                _feedForward.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Input, i));
                testError += _ouputDeviation.Compute(neuralNetwork, neuralNetworkTrainModel.GetValuesForLayer(NeuronLayer.Output, i));
            }
            neuralNetwork.NeuralNetworkError = testError / testSet;//update with test error
        }
    }
}
