namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface IMaths
    {
        double DerivativeMethod(double val);
        double OutputMethod(double val);

        double Random();
    }
}