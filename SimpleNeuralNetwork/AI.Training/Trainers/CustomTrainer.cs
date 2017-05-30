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
    public class CustomTrainer : AbstactTrainer, ITrainer
    {
        protected override NeuralNetworkTrainModel NeuralNetworkModel { get; } = new NeuralNetworkTrainModel();

        //*****************************************************************************
        //REMEMBER TO NORMALIZE YOUR DATA, 
        // VALUES MUST BE FROM -1 to +1 FOR HYPERTAN AND 0 TO 1 FOR SIGMOID
        //*****************************************************************************        

        public CustomTrainer(NeuralNetworkCompute neuralNetworkCompute, IDataRepository filehandle) : base(neuralNetworkCompute, filehandle)
        {
            //Values of Input neurons define variables inserted into the system
            //Values of Output neurons define the expected result of the neural network
            //Read the values of the model vertically to have the functions: f( .2, .1, .1 ) = [  0, .4 ] 
            //                                                               f( .3, .2, .1 ) = [  0, .6 ]
            //                                                               f( .2, .1, .2 ) = [-.1, .5 ]
            //                                                               f( .1, .1, .1 ) = [-.1, .3 ]
            //For example, Input neuron 1 will have as input '.2', neuron 2 will have '.1', and neuron 3 will have '.1'
            //Expected value for Output neuron 1 is '0' and for Output Neuron 2 is '.4'
            //Neural Network will try to replicate procedure f for every unknown input. That's what NN do :)
            NeuralNetworkModel = new NeuralNetworkModeling()
                                        .SetHiddenNeurons(5)                                //Set the number of hidden neurons
                                        .SetMathFunctions(MathFunctions.HyperTan)           //Set the algorithms to be used 
                                        .SetAcceptedError(.001)                             //Set accepted error for the train session to complete

                                        .AddInputNeuron()                                   //Add Input Neuron 1
                                        .AddValue(.2).AddValue(.3).AddValue(.2).AddValue(.1)//Add values for the Input Neuron 1
                                        .AddInputNeuron()                                   //Add Input Neuron 2
                                        .AddValue(.1).AddValue(.2).AddValue(.1).AddValue(.1)//Add values for the Input Neuron 2
                                        .AddInputNeuron()                                   //Add an Input Neuron 3
                                        .AddValue(.1).AddValue(.1).AddValue(.2).AddValue(.1)//Add values for the Input Neuron 3

                                        .AddOutputNeuron()                                  //Add Output Neuron 1
                                        .AddValue(0).AddValue(0).AddValue(-.1).AddValue(-.1)//Add Expected values for the Output Neuron 1
                                        .AddOutputNeuron()                                  //Add Output Neuron 2
                                        .AddValue(.4).AddValue(.6).AddValue(.5).AddValue(.3)//Add Expected values for the Output Neuron 2

                                        .Get();                                             //Get the model
        }
    }
}
