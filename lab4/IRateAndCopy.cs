namespace lab4
{
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}