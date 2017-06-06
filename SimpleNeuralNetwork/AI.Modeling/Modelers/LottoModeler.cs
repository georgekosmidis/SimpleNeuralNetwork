using SimpleNeuralNetwork.AI.Modeling.Interfaces;
using SimpleNeuralNetwork.AI.Modeling.Modelers.ModelingHelpers;
using SimpleNeuralNetwork.AI.Modeling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Modeling.Modelers
{
    public class LottoModeler : IModeler
    {
        public NeuralNetworkTrainModel NeuralNetworkModel { get; }

        private string pathToResults = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "AI.Modeling" + Path.DirectorySeparatorChar + "Modelers" + Path.DirectorySeparatorChar + "LottoModelerData" + Path.DirectorySeparatorChar + "lotto_results.csv";

        public LottoModeler()
        {

            var lines = File.ReadAllLines(pathToResults);
            var inputs = new double[49][];
            var outputs = new double[49][];

            //initialize
            for (var i = 0; i < 49; i++)
            {
                inputs[i] = new double[lines.Length];
                outputs[i] = new double[lines.Length];
                for (var j = 0; j < lines.Length; j++)
                {
                    inputs[i][j] = i + 1;
                    outputs[i][j] = 0;
                }
            }

            //set output
            for (var j = 0; j < lines.Length; j++)
            {
                var numbers = lines[j].Split(';');
                for (var k = 0; k < numbers.Length; k++)
                {
                    outputs[Convert.ToInt32(numbers[k]) - 1][j] = 1d;
                }
            }



            var modelCreate = new NeuralNetworkTrainModelCreate()
                                        //.AutoAdjustHiddenLayer()
                                        .AddHiddenLayers( x=> x.AddHiddenLayer(20).AddHiddenLayer(20))
                                        .SetMathFunctions( AI.Models.MathFunctions.Sigmoid)
                                        .SetAcceptedError(.02)
                                        .SetNeuralNetworkName("Lotto");
            foreach (var input in inputs)
                modelCreate.AddInputNeuron(x => x.AddValues(input));

            foreach (var output in outputs)
                modelCreate.AddOutputNeuron(x => x.AddValues(output));


            NeuralNetworkModel = modelCreate.Get();
        }
    }
}
