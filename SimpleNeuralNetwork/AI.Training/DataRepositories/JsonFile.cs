using Newtonsoft.Json;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Repositories
{
    public class JsonFile : IDataRepository
    {
        private string _folder;

        public JsonFile(string folder)
        {
            _folder = folder;
        }

        public void Save(string fileName, NeuralNetwork neuralNetwork)
        {
            TextWriter writer = null;

            var json = JsonConvert.SerializeObject(neuralNetwork, Formatting.None,
                                                        new JsonSerializerSettings
                                                        {
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        });
            writer = new StreamWriter(_folder + Path.DirectorySeparatorChar + fileName, false);
            writer.Write(json);

            writer.Close();

        }
        public NeuralNetwork Load(string fileName)
        {
            var reader = new StreamReader(_folder + Path.DirectorySeparatorChar + fileName);
            var json = reader.ReadToEnd();
            reader.Close();
            var neuralNetwork = JsonConvert.DeserializeObject<NeuralNetwork>(json);

            return ReviveReferences(neuralNetwork);
        }

        private NeuralNetwork ReviveReferences(NeuralNetwork neuralNetwork)
        {

            for (var i = 0; i < neuralNetwork.InputNeurons.Count(); i++)
            {
                for (var j = 0; j < neuralNetwork.InputNeurons[i].OutputSynapses.Count(); j++)
                {
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
