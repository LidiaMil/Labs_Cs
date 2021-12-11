namespace lab5
{
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}