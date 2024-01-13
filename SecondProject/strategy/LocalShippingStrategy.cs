namespace SecondProject.strategy
{
    public class LocalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateFinalTotal(decimal orderTotal)
        {
            return orderTotal + 10;
        }
    }
}
