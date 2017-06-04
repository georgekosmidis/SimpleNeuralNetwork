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
                    savedNeuralNetwork.InputHiddenSynapsis.Add(new SavedSynapsis()
                    {
                        Index = synapsis.Index,
                        Weight = synapsis.Weight,
                        FromNeuronIndex = synapsis.FromNeuron.Index,
                        ToNeuronIndex = synapsis.ToNeuron.Index
                    });
                }
            }

            foreach (var neuron in neuralNetwork.HiddenNeurons)
            {
                savedNeuralNetwork.HiddenNeurons.Add(new SavedNeuron()
                {
                    Index = neuron.Index,
                    Value = neuron.Value,
                    Error = neuron.Error
                });
                foreach (var synapsis in neuron.OutputSynapses)
                {
                    savedNeuralNetwork.HiddenOutputSynapsis.Add(new SavedSynapsis()
                    {
                        Index = synapsis.Index,
                        Weight = synapsis.Weight,
                        FromNeuronIndex = synapsis.FromNeuron.Index,
                        ToNeuronIndex = synapsis.ToNeuron.Index
                    });
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
            var reader = new StreamReader(_folder + Path.DirectorySeparatorChar + name + ".json");
            var json = reader.ReadToEnd();
            reader.Close();
            var savedNeuralNetwork = JsonConvert.DeserializeObject<SavedNeuralNetwork>(json);

            var neuralNetwork = new NeuralNetwork();
            neuralNetwork.MathFunctions = savedNeuralNetwork.MathFunctions;
            neuralNetwork.Divisor = savedNeuralNetwork.Divisor;

            foreach (var savedNeuron in savedNeuralNetwork.InputNeurons)
                neuralNetwork.InputNeurons.Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });

            foreach (var savedNeuron in savedNeuralNetwork.HiddenNeurons)
                neuralNetwork.HiddenNeurons.Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });

            foreach (var savedNeuron in savedNeuralNetwork.OutputNeurons)
                neuralNetwork.OutputNeurons.Add(new Neuron() { Index = savedNeuron.Index, Value = savedNeuron.Value, Error = savedNeuron.Error });

            foreach (var savedSynapsis in savedNeuralNetwork.InputHiddenSynapsis)
            {

                var fromNeuron = neuralNetwork.InputNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex);
                var toNeuron = neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.ToNeuronIndex);

                var synapse = new Synapse(neuralNetwork.InputNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex), neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.ToNeuronIndex))
                {
                    Index = savedSynapsis.Index,
                    Weight = savedSynapsis.Weight
                };

                neuralNetwork.InputNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex).OutputSynapses.Add(synapse);
                neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.ToNeuronIndex).InputSynapses.Add(synapse);

            }

            foreach (var savedSynapsis in savedNeuralNetwork.HiddenOutputSynapsis)
            {

                var fromNeuron = neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex);
                var toNeuron = neuralNetwork.OutputNeurons.FirstOrDefault(x => x.Index == savedSynapsis.ToNeuronIndex);
                if (toNeuron == null)
                    continue;
                var synapse = new Synapse(neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex), neuralNetwork.OutputNeurons.FirstOrDefault(x => x.Index == savedSynapsis.ToNeuronIndex))
                {
                    Index = savedSynapsis.Index,
                    Weight = savedSynapsis.Weight
                };

                neuralNetwork.HiddenNeurons.First(x => x.Index == savedSynapsis.FromNeuronIndex).OutputSynapses.Add(synapse);
                neuralNetwork.OutputNeurons.FirstOrDefault(x => x.Index == savedSynapsis.ToNeuronIndex).InputSynapses.Add(synapse);

            }
            
            return neuralNetwork;
        }

        private NeuralNetwork ReviveReferences(NeuralNetwork neuralNetwork)
        {

            for (var i = 0; i < neuralNetwork.InputNeurons.Count(); i++)
            {
                for (var j = 0; j < neuralNetwork.InputNeurons.ToArray()[i].OutputSynapses.Count(); j++)
                {
                    neuralNetwork.InputNeurons.ElementAt(i).OutputSynapses.ElementAt(j).FromNeuron = neuralNetwork.InputNeurons.ElementAt(i);
                    neuralNetwork.InputNeurons.ElementAt(i).OutputSynapses.ElementAt(j).ToNeuron = neuralNetwork.HiddenNeurons.ElementAt(j);

                    neuralNetwork.HiddenNeurons.ElementAt(j).InputSynapses.ElementAt(i).FromNeuron = neuralNetwork.InputNeurons.ElementAt(i);
                    neuralNetwork.HiddenNeurons.ElementAt(j).InputSynapses.ElementAt(i).ToNeuron = neuralNetwork.HiddenNeurons.ElementAt(j);
                }
            }

            for (var i = 0; i < neuralNetwork.HiddenNeurons.Count(); i++)
            {
                for (var j = 0; j < neuralNetwork.HiddenNeurons.ElementAt(i).OutputSynapses.Count(); j++)
                {
                    neuralNetwork.HiddenNeurons.ElementAt(i).OutputSynapses.ElementAt(j).FromNeuron = neuralNetwork.HiddenNeurons.ElementAt(i);
                    neuralNetwork.HiddenNeurons.ElementAt(i).OutputSynapses.ElementAt(j).ToNeuron = neuralNetwork.OutputNeurons.ElementAt(j);

                    neuralNetwork.OutputNeurons.ElementAt(j).InputSynapses.ElementAt(i).FromNeuron = neuralNetwork.HiddenNeurons.ElementAt(i);
                    neuralNetwork.OutputNeurons.ElementAt(j).InputSynapses.ElementAt(i).ToNeuron = neuralNetwork.OutputNeurons.ElementAt(j);
                }
            }

            return neuralNetwork;
        }
    }
}
