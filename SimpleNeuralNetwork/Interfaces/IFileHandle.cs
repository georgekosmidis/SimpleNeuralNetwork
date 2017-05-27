namespace SimpleNeuralNetwork.Interfaces
{
    public interface IFileHandle
    {
        T Read<T>(string filePath) where T : new();
        void Write<T>(string filePath, T obj, bool append = false) where T : new();
    }
}