using Newtonsoft.Json;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.BrainRepositories.JsonFileHelpers.Models;
using SimpleNeuralNetwork.AI.Interfaces;

namespace SimpleNeuralNetwork.AI.BrainRepositories
{
    public class JsonFile : IBrainRepository
    {
        private string _folder;

        public JsonFile(string folder)
        {
            _folder = folder + Path.DirectorySeparatorChar + "AI" + Path.DirectorySeparatorChar + "TrainedNetworks" + Path.DirectorySeparatorChar;
        }

        public void Save(string name, NeuralNetwork neuralNetwork)
        {
            var savedNeuralNetwork = new SavedNeuralNetwork();
            savedNeuralNetwork.MathFunctions = neuralNetwork.MathFunctions;
            savedNeuralNetwork.Divisor = neuralNetwork.Divisor;

            foreach (var neuron in neuralNetwork.InputNeurons)
            {
                savedNeuralNetwork.InputNeurons.Add(new SavedNeuron()
                {
                    Index = neuron.Index,
                    Value = neuron.Value,
                    Error = neuron.Error
                });

                foreach (var synapsis in neuron.OutputSynapses)
                {
                    savedNeuralNetwork.Synapsis.Add(new SavedSynapsis()
                    {
                        Index = synapsis.Index,
                        Weight = synapsis.Weight,
                        FromNeuronIndex = synapsis.FromNeuron.Index,
                        ToNeuronIndex = synapsis.ToNeuron.Index
                    });
                }
            }

            for (var i = 0; i < neuralNetwork.HiddenLayers.Count(); i++)
            {
                savedNeuralNetwork.HiddenLayers.Add(new List<SavedNeuron>());
                for (var j = 0; j < neuralNetwork.HiddenLayers[i].Count(); j++)
                {
                    savedNeuralNetwork.HiddenLayers[i].Add(new SavedNeuron()
                    {
                        Index = neuralNetwork.HiddenLayers[i][j].Index,
                        Value = neuralNetwork.HiddenLayers[i][j].Value,
                        Error = neuralNetwork.HiddenLayers[i][j].Error
                    });
                    foreach (var synapsis in neuralNetwork.HiddenLayers[i][j].OutputSynapses)
                    {
                        savedNeuralNetwork.Synapsis.Add(new SavedSynapsis()
                        {
                            Index = synapsis.Index,
                            Weight = synapsis.Weight,
                            FromNeuronIndex = synapsis.FromNeuron.Index,
                            ToNeuronIndex = synapsis.ToNeuron.Index
                        });
                    }
                }

            }
            foreach (var neuron in neuralNetwork.OutputNeurons)
            {
                savedNeuralNetwork.OutputNeurons.Add(new SavedNeuron()
                {
                    Index = neuron.Index,
                    Value = neuron.Value,
                    Error = neuron.Error
                });
            }


            TextWriter writer = null;
            var json = JsonConvert.SerializeObject(savedNeuralNetwork, Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                                                        });
            writer = new StreamWriter(_folder + Path.DirectorySeparatorChar + name + ".json", false);
            writer.Write(json);

            writer.Close();

        }
        public NeuralNetwork Load(string name)
        {
            if (!File.Exists(_folder + Path.DirectorySeparatorChar + name + ".json"))
                throw new FileNotFoundException("File not found: " + _folder + Path.DirectorySeparatorChar + name + ".json" + Environment.NewLine + "Have you saved it after training?");
            var reader = new StreamReader(_folder + Path.DirectorySeparatorChar + name + ".json");
            var json = reader.ReadToEnd();
            reader.Close();
            var savedNeuralNetwork = JsonConvert.DeserializeObject<SavedNeuralNetwork>(json);

            var neuralNetwork = new NeuralNetwork();
            neuralNetwork.MathFunctions = savedNeuralNetwork.MathFunctions;
            neuralNetwork.Divisor = savedNeuralNetwork.Divisor;

            foreach (var savedNeuron in savedNeuralNetwork.InputNeurons)
                neuralNetwork.InputNeurons.Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });

            for (var i = 0; i < savedNeuralNetwork.HiddenLayers.Count(); i++)
            {
                neuralNetwork.HiddenLayers.Add(new List<Neuron>());
                foreach (var savedNeuron in savedNeuralNetwork.HiddenLayers[i])
                    neuralNetwork.HiddenLayers[i].Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });
            }

            foreach (var savedNeuron in savedNeuralNetwork.OutputNeurons)
                neuralNetwork.OutputNeurons.Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });

            foreach (var savedSynapsis in savedNeuralNetwork.Synapsis)
            {
                var neurons = neuralNetwork.InputNeurons.Concat(neuralNetwork.OutputNeurons);
                foreach (var layer in neuralNetwork.HiddenLayers)
                    neurons = neurons.Concat(layer);


                var fromNeuron = neurons.First(x => x.Index == savedSynapsis.FromNeuronIndex);
                var toNeuron = neurons.First(x => x.Index == savedSynapsis.ToNeuronIndex);

                var synapse = new Synapse(fromNeuron, toNeuron)
                {
                    Index = savedSynapsis.Index,
                    Weight = savedSynapsis.Weight
                };

                fromNeuron.OutputSynapses.Add(synapse);
                toNeuron.InputSynapses.Add(synapse);

            }

            return neuralNetwork;
        }

    }
}
