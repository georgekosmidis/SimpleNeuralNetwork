# Simple Neural Network in C#
### AI
A multi-layer perceptron (one input, one hidden and output), forward feed neural network with backward propagation and x number of neurons for each layer.
#### Characteristics
* 1 Input, 1 Hidden, 1 Output layer
* Unlimited neurons per layer
* Forward feeding, backward propagation
* 2 Methods, HyperTan / Sigmoid
* Set number of hidden neurons or allow automatic adjustment to find the optimal solution
* Samples are devided in Train data, Verification data and Test data 
* Save / Load model from disk
* Custom Modeler with instuctions (_AI.Modeling/Modelers/CustomModeler.cs_)
* One complete model with 3 input neurons and 2 output neurons (_AI.Modeling/Modelers/AddSubtractModeler.cs_)
	* Output Neuron 1 substracts the three input values, 
	* Output Neuron 2 adds them 
* One model with low accuracy to calculate XOR (_AI.Modeling/Modelers/XORModeler.cs_)

### Math Methods
Two available methods depending on the model.<br />
* _Sigmoid_ as output method is in in the range of 0 to 1, so input/ouput data must me normalized  from 0 to 1
* _HyperTan_ is in in the range of -1 to 1, so input/ouput data must me normalized from -1 to 1

### Program.cs
Working example of how to train the Neural Network to add and substract three decimals
### How to
Use NeuralNetworkFactoryHelper to train a neural network or to load a pre-trained one:
```csharp
var factoryHelper = new NeuralNetworkFactoryHelper( [PATH_TO_SAVE_OR_LOAD_TRAINED_NETWORKS] );
var neuralNetwork = factoryHelper.Train( [NeuralNetworkFactory.NetworkFor.AddSubtract | NeuralNetworkFactory.NetworkFor.XOR | NeuralNetworkFactory.NetworkFor.Custom], [ true | false ] );
//OR
var neuralNetwork = factoryHelper.Load( [NeuralNetworkFactory.NetworkFor.AddSubtract | NeuralNetworkFactory.NetworkFor.XOR | NeuralNetworkFactory.NetworkFor.Custom] );
```
Test NN efficiency by trying unknown numbers as variabes with Run:
```csharp
var result = neuralNetwork.Run( 2, 2, 1 );
```
Use Custom Model in AI.Modeling/Modelers/CustomModeler.cs to model your own problem.
```csharp
//Values of Input neurons define variables inserted into the system
//Values of Output neurons define the expected result of the neural network
//Read the values of the model vertically to have the functions: 
//                                                               f( 2, 1, 1 ) = [  0, 4 ] 
//                                                               f( 3, 2, 1 ) = [  0, 6 ]
//                                                               f( 2, 1, 2 ) = [ -1, 5 ]
//                                                               f( 1, 1, 1 ) = [ -1, 3 ]
//                                     Values for Input Neuron 1-----^   
//                                     Values for Input Neuron 2---------^
//                                     Values for Input Neuron 3-------------^
//                                    Values for Output Neuron 1----------------------^
//                                    Values for Output Neuron 2--------------------------^
//For example, Input neuron 1 will have as input '2', neuron 2 will have '1', and neuron 3 will have '1'
//Expected value for Output neuron 1 is '0' and for Output Neuron 2 is '4'
//Neural Network will try to replicate procedure f for every unknown input. That's what NNs do :)
NeuralNetworkModel = new NeuralNetworkTrainModelCreate()

			//-----------------------------------------------------------------------------------
			.SetMathFunctions(MathFunctions.HyperTan)           //Set the algorithms to be used                         
			.SetHiddenNeurons(5)                                //Set the number of hidden neurons
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
```

## Output after training
![Results](https://raw.githubusercontent.com/georgekosmidis/SimpleNeuralNetwork/master/README/Capture.PNG)

