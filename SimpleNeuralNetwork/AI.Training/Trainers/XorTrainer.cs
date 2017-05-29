using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Models;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Trainers
{
    public class XorTrainer : AbstactTrainer, ITrainer
    {
        protected override NeuralNetworkTrainModel NueralNetworkModel { get; } = new NeuralNetworkTrainModel();


        public XorTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataHandle filehandle) : base(neuralNetworkCompute, filehandle)
        {
            NueralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)
                                        .SetMathFunctions(MathFunctions.Sigmoid)
                                        .SetAcceptedError(.001)

                                        .AddInputNeuron()
                                        .AddValue(0).AddValue(1).AddValue(0).AddValue(1)
                                        .AddInputNeuron()
                                        .AddValue(0).AddValue(0).AddValue(1).AddValue(1)

                                        .AddOutputNeuron()
                                        .AddValue(0).AddValue(1).AddValue(1).AddValue(0)
                                        
                                        .Get();
        }
    }
}
