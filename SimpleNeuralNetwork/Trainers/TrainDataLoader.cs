using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Trainers
{
    public class TrainDataLoader
    {
        IFileHandle _filehandle;

        public TrainDataLoader(IFileHandle filehandle)
        {
            _filehandle = filehandle;
        }

        public NeuralNetwork Load(string filename)
        {
            var neuralNetwork = _filehandle.Read<NeuralNetwork>(filename);

            //revive from deep copy
            for (var i = 0; i < neuralNetwork.InputNeurons.Count(); i++)
            {
                for (var j = 0; j < neuralNetwork.InputNeurons[i].OutputSynapses.Count(); j++)
                {
                    //neuralNetwork.InputNeurons[i].OutputSynapses[j] = neuralNetwork.HiddenNeurons[j].InputSynapses[i];

                    neuralNetwork.InputNeurons[i].OutputSynapses[j].FromNeuron = neuralNetwork.InputNeurons[i];
                    neuralNetwork.InputNeurons[i].OutputSynapses[j].ToNeuron = neuralNetwork.HiddenNeurons[j];

                    neuralNetwork.HiddenNeurons[j].InputSynapses[i].FromNeuron = neuralNetwork.InputNeurons[i];
                    neuralNetwork.HiddenNeurons[j].InputSynapses[i].ToNeuron = neuralNetwork.HiddenNeurons[j];
                }
            }

            for (var i = 0; i < neuralNetwork.HiddenNeurons.Count(); i++)
            {
                for (var j = 0; j < neuralNetwork.HiddenNeurons[i].OutputSynapses.Count(); j++)
                {
                    //neuralNetwork.HiddenNeurons[i].OutputSynapses[j] = neuralNetwork.OutputNeurons[j].InputSynapses[i];

                    neuralNetwork.HiddenNeurons[i].OutputSynapses[j].FromNeuron = neuralNetwork.HiddenNeurons[i];
                    neuralNetwork.HiddenNeurons[i].OutputSynapses[j].ToNeuron = neuralNetwork.OutputNeurons[j];

                    neuralNetwork.OutputNeurons[j].InputSynapses[i].FromNeuron = neuralNetwork.HiddenNeurons[i];
                    neuralNetwork.OutputNeurons[j].InputSynapses[i].ToNeuron = neuralNetwork.OutputNeurons[j];
                }
            }


            return neuralNetwork;
        }

    }
}
