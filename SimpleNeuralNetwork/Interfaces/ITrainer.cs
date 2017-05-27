namespace SimpleNeuralNetwork.Interfaces
{
    public interface ITrainer
    {
        void Save(string filename);
        void Train(int hiddenLayerNeurons, double acceptedError);
    }
}