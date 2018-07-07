namespace SimpleNeuralNetwork.Modeler.Interfaces
{
    public interface INeuronValuesCreator
    {
        double Divisor { get; }

        INeuronValuesCreator AddValues(params double[] values);
    }
}