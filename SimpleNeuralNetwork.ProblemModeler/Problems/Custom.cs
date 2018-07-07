using SimpleNeuralNetwork.Modeler.Interfaces;
using SimpleNeuralNetwork.ProblemModeler.Creators;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Modeler.Problems
{
    public class Custom :  IProblem, IProblemCustom
    {
        //***************************************************************************************
        //REMEMBER TO NORMALIZE YOUR DATA
        //SAMPLES MUST BE AS MUCH AS POSSIBLE!!! 
        //      This example has only 4 samples, so NN is doomed to have low accuracy
        //      You can check AddSubstractModeler for a complete example
        //***************************************************************************************

        public ProblemDescriptionModel Get()
        {
            //Values of Input neurons define variables inserted into the system
            //Values of Output neurons define the expected result of the neural network
            //Read the values of the model vertically to have the functions: 
            //                                                               f( 2, 1, 1 ) = [  0, 4 ] 
            //                                                               f( 3, 2, 1 ) = [  0, 6 ]
            //                                                               f( 2, 1, 2 ) = [ -1, 5 ]
            //                                                               f( 1, 1, 1 ) = [ -1, 3 ]
            //                                     Values for Input Neuron 1----^   
            //                                     Values for Input Neuron 2------^
            //                                     Values for Input Neuron 3---------^
            //                                    Values for Output Neuron 1------------------^
            //                                    Values for Output Neuron 2--------------------^
            //For example, Input neuron 1 will have as input '2', neuron 2 will have '1', and neuron 3 will have '1'
            //Expected value for Output neuron 1 is '0' and for Output Neuron 2 is '4'
            //Neural Network will try to replicate procedure f for every unknown input. That's what NN do :)
            return new ProblemDescriptionCreator()
                //-----------------------------------------------------------------------------------
                //.SetMathFunctions(MathFunctions.HyperTan)         //Set the algorithms to be used                         
                //.SetHiddenNeurons(5)                              //Set the number of hidden neurons
                //--OR--                                
                .AutoAdjustHiddenLayer()                            //Let the network handle hidden neurons in order to find optimal solution
                //-----------------------------------------------------------------------------------

                .SetAcceptedError(.02)                              //Set accepted error for the train session to complete, current is 1%
                .SetNeuralNetworkName("Custom")                     //Set Network Name

                .AddInputNeuron(x => x.AddValues(2, 3, 2, 1))       //Add Input Neuron 1
                .AddInputNeuron(x => x.AddValues(1, 2, 1, 1))       //Add Input Neuron 2
                .AddInputNeuron(x => x.AddValues(1, 1, 2, 1))       //Add an Input Neuron 3

                .AddOutputNeuron(x => x.AddValues(0, 0, -1, -1))    //Add Output Neuron 1
                .AddOutputNeuron(x => x.AddValues(4, 6, 5, 3))      //Add Output Neuron 2

                .Get();                                             //Get the model
        }
    }
}
