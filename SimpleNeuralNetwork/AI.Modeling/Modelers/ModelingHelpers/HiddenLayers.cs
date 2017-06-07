using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers
{
    public class HiddenLayers
    {
        List<HiddenLayerModel> _hiddenLayers;

        public HiddenLayers(List<HiddenLayerModel> hiddenLayers)
        {
            _hiddenLayers = hiddenLayers;
        }

        public HiddenLayers AddHiddenLayer(int neuronsCount)
        {
            var hiddenLayer = new HiddenLayerModel();
            hiddenLayer.NeuronsCount = neuronsCount;
            _hiddenLayers.Add(hiddenLayer);
            return this;
        }
    }
}
