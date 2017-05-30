﻿using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleNeuralNetwork.AI.Modeling.Modelers;
using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using SimpleNeuralNetwork.EventArguments;
using SimpleNeuralNetwork.AI.BrainRepositories;
using System.Globalization;

namespace SimpleNeuralNetwork.Helpers
{
    public class NeuralNetworkFactoryHelper
    {
        string _trainedNetworksPath;
        public NeuralNetworkFactoryHelper(string trainedNetworksPath)
        {
            _trainedNetworksPath = trainedNetworksPath;
        }

        public enum NetworkFor { Addition, XOR, Custom }

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public AI.NeuralNetworkFactory.Runner Train(NetworkFor networkFor)
        {
            var neuralNetworkFactory = new AI.NeuralNetworkFactory(
                                          new NeuralNetworkRepository(
                                              new JsonFile(_trainedNetworksPath)
                                          ),
                                          new NeuralNetworkRunner(
                                               new FeedForward()
                                          ),
                                          new NeuralNetworkTrainer(
                                              new FeedForward(),
                                              new BackPropagate(),
                                              new NetworkLayers(
                                                  new NeuronSynapsis()
                                              )
                                          )
                                      );

            neuralNetworkFactory.OnNewIteration += NeuralNetworkFactory_OnNewIteration;
            neuralNetworkFactory.OnSampleLearned += NeuralNetworkFactory_OnSampleLearned;

            IModeler modeler;
            switch (networkFor)
            {
                case NetworkFor.Addition:
                    modeler = new AdditionModeler();
                    break;
                case NetworkFor.XOR:
                    modeler = new XorModeler();
                    break;
                case NetworkFor.Custom:
                    modeler = new CustomModeler();
                    break;
                default:
                    throw new NotImplementedException("Network " + networkFor + " not implemented!");
            }

            var runner = neuralNetworkFactory.Train(modeler.NeuralNetworkModel);
            neuralNetworkFactory.Save();

            return runner;
        }

        public AI.NeuralNetworkFactory.Runner Load(NetworkFor networkFor)
        {
            return new AI.NeuralNetworkFactory(
                            new NeuralNetworkRepository(
                                new JsonFile(_trainedNetworksPath)
                            ),
                            new NeuralNetworkRunner(
                                new FeedForward()
                            ),
                            new NeuralNetworkTrainer(
                                new FeedForward(),
                                new BackPropagate(),
                                new NetworkLayers(
                                    new NeuronSynapsis()
                                )
                            )
                        ).Load(networkFor.ToString());

        }

        private void NeuralNetworkFactory_OnSampleLearned(object sender, AI.EventArguments.SampleEventArgs e)
        {
            var s = new StringBuilder();
            for (var i = 0; i < e.Expected.Length; i++)
            {
                s.Append(
                String.Format("{0,10} |{1,10} |{2,10} |{3,10}",
                               (i + 1),
                               e.Expected[i].ToString("0.0000", CultureInfo.InvariantCulture),
                               e.Actual[i].ToString("0.0000", CultureInfo.InvariantCulture),
                               e.Error[i].ToString("0.0000", CultureInfo.InvariantCulture)
                            )
                );
                s.Append(Environment.NewLine);
            }

            OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(s.ToString()));
        }

        private void NeuralNetworkFactory_OnNewIteration(object sender, AI.EventArguments.IterationEventArgs e)
        {
            var s = new StringBuilder();
            s.AppendLine(new String('*', 50));
            s.AppendLine("Starting Iteration " + e.Iteration);
            s.AppendLine(new String('-', 50));
            s.AppendLine(String.Format("{0,10} |{1,10} |{2,10} |{3,10}", "Neuron", "Expected", "Actual", "Error"));

            OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(s.ToString()));
        }
    }
}