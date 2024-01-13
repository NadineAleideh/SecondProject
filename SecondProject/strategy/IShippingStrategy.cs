namespace SecondProject.strategy
{
    public interface IShippingStrategy
    {
        decimal CalculateFinalTotal(decimal orderTotal);
    }
}
