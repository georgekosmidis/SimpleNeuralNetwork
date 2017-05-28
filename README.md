# Simple Neural Network in C#
### AI
A multi-layer perceptron (one input, one hidden and output), forward feed neural network with backward propagation and x number of neurons for each layer.
### Trainers
Two trainers, for addition and XOR, with the ability to live train the neural network or load a trained data file into the network.
### Math Methods
Two available methods, Sigmoid and HyperTan to try train the network and see the differences
### Program.cs
Working example of how to train the Nueral Network to add to decimals, or to load trained data.
### How to
Use NeuralNetworkFactory to get a new nueral network:
```csharp
var neuralNetwork = factory.Get( [NeuralNetworkFactory.NetworkFor.Addition | NeuralNetworkFactory.NetworkFor.XOR], 
                                 [NeuralNetworkFactory.TrainType.LiveTraining | NeuralNetworkFactory.NetworkFor.Trained], 
                                 [NeuralNetworkFactory.MathMethods.Sigmoid | NeuralNetworkFactory.MathMethods.HyperTan] );
```
Test NN efficiency by trying unknown numbers as variabes with Compute:
```csharp
var result = neuralNetwork.Compute(new double[] { .3, .2 });
```

_Sigmoid/HyperTan as output methods is in in the range of 0 to 1, so input/ouput data must me normalized to be lower than .1_

## Output after training
![Results](https://raw.githubusercontent.com/georgekosmidis/SimpleNeuralNetwork/master/README/Capture.PNG)

