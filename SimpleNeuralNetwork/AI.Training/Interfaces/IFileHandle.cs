namespace SimpleNeuralNetwork.AI.Training.Interfaces
{
    public interface IDataRepository
    {
        T Load<T>(string filePath) where T : new();
        void Save<T>(string filePath, T obj, bool append = false) where T : new();
    }
}