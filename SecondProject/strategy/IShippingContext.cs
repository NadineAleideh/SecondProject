namespace SecondProject.strategy
{
    public interface IShippingContext
    {
        void SetStrategy(IShippingStrategy strategy);

        decimal ExecuteStrategy(decimal orderTotal);
    }
}
