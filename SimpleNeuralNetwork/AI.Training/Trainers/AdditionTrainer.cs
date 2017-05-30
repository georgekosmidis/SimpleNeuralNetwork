using SimpleNeuralNetwork.AI;
using SimpleNeuralNetwork.AI.Models;
using SimpleNeuralNetwork.AI.Training.Interfaces;
using SimpleNeuralNetwork.AI.Training.Models;
using SimpleNeuralNetwork.AI.Training.Trainers.ModelingHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Training.Trainers
{
    public class AdditionTrainer : AbstactTrainer, ITrainer
    {

        protected override NeuralNetworkTrainModel NeuralNetworkModel { get; } = new NeuralNetworkTrainModel();

        public AdditionTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataRepository filehandle) : base(neuralNetworkCompute, filehandle)
        {
            NeuralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)
                                        .SetMathFunctions(MathFunctions.Sigmoid)
                                        .SetAcceptedError(.001)

                                        .AddInputNeuron()
                                        .AddValue(.1).AddValue(.3).AddValue(.1).AddValue(.1)
                                        .AddInputNeuron()
                                        .AddValue(.2).AddValue(.1).AddValue(.4).AddValue(.1)
                                        
                                        .AddOutputNeuron()
                                        .AddValue(.3).AddValue(.4).AddValue(.5).AddValue(.2)
                                        
                                        .Get();
        }
    }
}
