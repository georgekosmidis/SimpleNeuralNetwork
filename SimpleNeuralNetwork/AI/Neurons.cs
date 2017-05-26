using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class Neurons : List<Neuron>
    {

        public void SetWeights(int weightCount)
        {
            Random rnd = new Random();
            var weights = new double[weightCount];
            for (var i = 0; i < weightCount; i++)
                weights[i] = rnd.Next(0, 100) / (double)100;

            foreach (var n in this)
                n.Weight = weights;
        }
        //public Neurons()
        //{

        //}

        //public new void Add(Neuron newron)
        //{
        //    base.Add(newron);
        //}

        //public new void Insert(int index, Neuron newron)
        //{
        //    base.Insert(index, newron);
        //}

        //public Neuron GetNeuron(int index)
        //{
        //    return base[index];
        //}
    }
}
