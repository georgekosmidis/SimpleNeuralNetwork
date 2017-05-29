namespace SimpleNeuralNetwork.AI.Training.Interfaces
{
    public interface ITrainer
    {
        void Save(string filename);
        void Train();
    }
}