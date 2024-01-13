namespace SecondProject.strategy
{
    public class WorldwideShippingStrategy : IShippingStrategy
    {
        public decimal CalculateFinalTotal(decimal orderTotal)
        {
            return orderTotal + 50;
        }
    }
}
