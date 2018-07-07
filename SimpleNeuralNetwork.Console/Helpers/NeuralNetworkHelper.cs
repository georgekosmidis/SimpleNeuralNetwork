using SimpleNeuralNetwork.Brain.Trainer.EventArguments;
using SimpleNeuralNetwork.Factories;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SimpleNeuralNetwork.Console.Helpers
{
    public class ConsoleHelper
    {

        public static NeuralNetwork TrainAndReturnNetwork<T>(bool saveTrainedNetwork) where T : class
        {
            var problem = new ProblemDescriptorFactory().Get<T>();

            //set up trainer
            var trainer = new NeuralNetworkTrainerFactory().Get();
            trainer.OnLearningCycleComplete += (object sender, LearningCycleCompleteEventArgs e) =>
            {
                System.Console.Write(
                    "\rOutput Error: " + e.Error.ToString("00.00000000000000000", CultureInfo.InvariantCulture) + " (After " + e.Iteration.ToString("00000") + " iterations...)"
                );
            };
            trainer.OnNetworkReconfigured += (object sender, NetworkReconfiguredEventArgs e) =>
            {
                var s = "";
                for (var i = 0; i < e.HiddenLayers.Count; i++)
                    s += "HL" + (i + 1) + "N" + e.HiddenLayers[i] + "-";
                s = s.Trim('-');

                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("Hidden Layers Setup: " + s);
                System.Console.WriteLine();
            };
            //get requested problem and train network
            var neuralNetwork = trainer.Train(problem);

            //save if requested
            if (saveTrainedNetwork)
            {
                new NeuralNetworkRepositoryFactory()
                    .Get()
                    .Save(neuralNetwork);
            }

            //report
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine(new String('=', 50));
            System.Console.WriteLine("Training Completed!");
            System.Console.WriteLine("Neural Network Accuracy: " + (100 - (Math.Round(neuralNetwork.NeuralNetworkError, 4) * 100)).ToString(CultureInfo.InvariantCulture) + "%");
            System.Console.WriteLine("Press a key to continue...");
            System.Console.ReadKey();

            return neuralNetwork;
        }

        public static NeuralNetwork LoadAndReturnNetwork<T>() where T : class
        {
            var problem = new ProblemDescriptorFactory().Get<T>();
            var repository = new NeuralNetworkRepositoryFactory().Get();
            return repository.Load(problem);
        }
    }
}
