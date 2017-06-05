using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Computations;
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
using SimpleNeuralNetwork.AI.EventArguments;
using SimpleNeuralNetwork.AI.NeuralNetworkTrainerHelpers;
using SimpleNeuralNetwork.AI.Computations.Maths;

namespace SimpleNeuralNetwork.Helpers
{
    public class NeuralNetworkFactoryHelper
    {
        string _trainedNetworksPath;
        public NeuralNetworkFactoryHelper(string trainedNetworksPath)
        {
            _trainedNetworksPath = trainedNetworksPath;
        }

        public enum NetworkFor { AddSubtract, XOR, Custom }

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        public AI.NeuralNetworkFactory.Runner Train(NetworkFor networkFor)
        {
            var neuralNetworkFactory = new AI.NeuralNetworkFactory(
                                          new NeuralNetworkRepository(
                                              new JsonFile(_trainedNetworksPath)
                                          ),
                                          new NeuralNetworkRunner(
                                               new FeedForward(
                                                   new MathFactory()
                                               )
                                          ),
                                          new NeuralNetworkTrainer(
                                              new NetworkLayers(
                                                  new NeuronSynapsis(),
                                                  new MathFactory()
                                              ),
                                              new TrainSet(
                                                  new FeedForward(
                                                      new MathFactory()
                                                  ),
                                                  new BackPropagate(
                                                      new MathFactory()
                                                  )
                                              ),
                                              new ValidationSet(
                                                  new FeedForward(
                                                      new MathFactory()
                                                  ),
                                                  new OuputDeviation()
                                              ),
                                              new TestSet(
                                                  new FeedForward(
                                                      new MathFactory()
                                                  ),
                                                  new OuputDeviation()
                                              )
                                          )
                                      );

            //neuralNetworkFactory.OnSampleLearned += NeuralNetworkFactory_OnSampleLearned;
            neuralNetworkFactory.OnLearningCycleComplete += NeuralNetworkFactory_OnLearningCycleComplete;
            neuralNetworkFactory.OnNetworkReconfigured += NeuralNetworkFactory_OnNetworkReconfigured;
            IModeler modeler;
            switch (networkFor)
            {
                case NetworkFor.AddSubtract:
                    modeler = new AddSubtractModeler();
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
            OnUpdateStatus?.Invoke(this, new ProgressEventArgs(Environment.NewLine + Environment.NewLine + new String('=', 50)));
            OnUpdateStatus?.Invoke(this, new ProgressEventArgs(Environment.NewLine + "Training Completed!"));
            //TODO: OnUpdateStatus?.Invoke(this, new ProgressEventArgs(Environment.NewLine + "Hidden Neurons: " + runner.NeuralNetwork.HiddenNeurons.Count()));
            OnUpdateStatus?.Invoke(this, new ProgressEventArgs(Environment.NewLine + "Neural Network Accuracy: " + (100 - (Math.Round(runner.NeuralNetwork.NeuralNetworkError, 4) * 100)).ToString(CultureInfo.InvariantCulture) + "%"));

            neuralNetworkFactory.Save();

            return runner;
        }


        public AI.NeuralNetworkFactory.Runner Load(NetworkFor networkFor)
        {
            return  new AI.NeuralNetworkFactory(
                        new NeuralNetworkRepository(
                            new JsonFile(_trainedNetworksPath)
                        ),
                        new NeuralNetworkRunner(
                            new FeedForward(
                                new MathFactory()
                            )
                        ),
                        new NeuralNetworkTrainer(
                            new NetworkLayers(
                                new NeuronSynapsis(),
                                new MathFactory()
                            ),
                            new TrainSet(
                                new FeedForward(
                                    new MathFactory()
                                ),
                                new BackPropagate(
                                    new MathFactory()
                                )
                            ),
                            new ValidationSet(
                                new FeedForward(
                                    new MathFactory()
                                ),
                                new OuputDeviation()
                            ),
                            new TestSet(
                                new FeedForward(
                                    new MathFactory()
                                ),
                                new OuputDeviation()
                            )
                        )
                    ).Load(networkFor.ToString());

        }

        private void NeuralNetworkFactory_OnNetworkReconfigured(object sender, NetworkReconfiguredEventArgs e)
        {
            var status = Environment.NewLine + Environment.NewLine + "Hidden Neurons: " + e.HiddenNeuronsCount + Environment.NewLine;
            OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(status));
        }

        private void NeuralNetworkFactory_OnLearningCycleComplete(object sender, LearningCycleCompleteEventArgs e)
        {
            var status = "\rOutput Error: " + e.Error.ToString("00.00000000000000000", CultureInfo.InvariantCulture) + " (After " + e.Iteration.ToString("00000") + " iterations...)";
            OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(status));
        }

        //private void NeuralNetworkFactory_OnSampleLearned(object sender, AI.EventArguments.SampleEventArgs e)
        //{
        //    var s = new StringBuilder();
        //    for (var i = 0; i < e.Expected.Length; i++)
        //    {
        //        s.Append(
        //        String.Format("{0,10} |{1,10} |{2,10} |{3,10}",
        //                       (i + 1),
        //                       e.Expected[i].ToString("0.0000", CultureInfo.InvariantCulture),
        //                       e.Actual[i].ToString("0.0000", CultureInfo.InvariantCulture),
        //                       e.Error[i].ToString("0.0000", CultureInfo.InvariantCulture)
        //                    )
        //        );
        //        s.Append(Environment.NewLine);
        //    }

        //    OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(s.ToString()));
        //}

        //private void NeuralNetworkFactory_OnNewIteration(object sender, AI.EventArguments.IterationEventArgs e)
        //{
        //    var s = new StringBuilder();
        //    s.AppendLine(new String('*', 50));
        //    s.AppendLine("Starting Iteration " + e.Iteration);
        //    s.AppendLine(new String('-', 50));
        //    s.AppendLine(String.Format("{0,10} |{1,10} |{2,10} |{3,10}", "Neuron", "Expected", "Actual", "Error"));

        //    OnUpdateStatus?.Invoke(sender, new ProgressEventArgs(s.ToString()));
        //}
    }
}
