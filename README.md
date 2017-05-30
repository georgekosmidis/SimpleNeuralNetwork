# Simple Neural Network in C#
### AI
A multi-layer perceptron (one input, one hidden and output), forward feed neural network with backward propagation and x number of neurons for each layer.
### Trainers
Two trainers, for addition and XOR, with the ability to live train the neural network or load a trained data file into the network.
### Math Methods
Two available methods, Sigmoid and HyperTan to try train the network and see the differences.

_Sigmoid as output method is in in the range of 0 to 1, so input/ouput data must me normalized  from 0 to 1_<br />
_HyperTan is in in the range of -1 to 1, so input/ouput data must me normalized from -1 to 1_

### Program.cs
Working example of how to train the Neural Network to add to decimals, or to load trained data.
### How to
Use NeuralNetworkFactoryHelper to train a neural network or to load a pre-trained one:
```csharp
var factoryHelper = new NeuralNetworkFactoryHelper(trainedNetworksPath);
var neuralNetwork = factoryHelper.Train( [NeuralNetworkFactory.NetworkFor.Addition | NeuralNetworkFactory.NetworkFor.XOR | NeuralNetworkFactory.NetworkFor.Custom] );
//OR
var  neuralNetwork = factoryHelper.Load( [NeuralNetworkFactory.NetworkFor.Addition | NeuralNetworkFactory.NetworkFor.XOR | NeuralNetworkFactory.NetworkFor.Custom] );
```
Test NN efficiency by trying unknown numbers as variabes with Compute:
```csharp
var result = neuralNetwork.Run(new double[] { .3, .2 });
```
Use Custom Trainer in AI.Training/Trainers/CustomTrainer.cs to model your own problem.
```csharp
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
				.SetNeuralNetworkName("Custom")                     //Set Network Name

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
```

## Output after training
![Results](https://raw.githubusercontent.com/georgekosmidis/SimpleNeuralNetwork/master/README/Capture.PNG)

