using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.ProblemModeler.Creators
{
    public class HiddenLayerCreator : IHiddenLayerCreator
    {
        List<ProblemDescriptionHiddenLayerModel> _hiddenLayers;

        public HiddenLayerCreator(List<ProblemDescriptionHiddenLayerModel> hiddenLayers)
        {
            _hiddenLayers = hiddenLayers;
        }

        public IHiddenLayerCreator AddHiddenLayer(int neuronsCount)
        {
            var hiddenLayer = new ProblemDescriptionHiddenLayerModel
            {
                NeuronsCount = neuronsCount
            };
            _hiddenLayers.Add(hiddenLayer);
            return this;
        }
    }
}
