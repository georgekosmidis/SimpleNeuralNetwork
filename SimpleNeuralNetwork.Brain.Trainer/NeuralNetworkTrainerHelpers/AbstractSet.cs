using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Trainer.NeuralNetworkTrainerHelpers
{
    public abstract class AbstractSet
    {

        protected int[] Suffle(int from, int next)
        {
            var suffle = new int[next];
            for (var i = 0; i < next; i++)
                suffle[i] = from + i;
            var rnd = new Random(DateTime.Now.Ticks.GetHashCode());
            return suffle.OrderBy(x => rnd.Next()).ToArray();
        }
    }
}
