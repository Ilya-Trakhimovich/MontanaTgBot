namespace MontanaTgBot.Models
{
    internal class Cart
    {
        public double TotalSum { get; set; }

        public IList<string>? Order { get; set; }

        public Cart()
        {
            TotalSum = 0;
            Order = new List<string>();
        }

        public void AddToCart(string dish)
        {
            if (!string.IsNullOrEmpty(dish) || !string.IsNullOrWhiteSpace(dish)) Order?.Add(dish);
        }

        public void RemoveFromCart(string dish)
        {
            if (Order.Contains(dish))
                Order.Remove(dish);
        }

        public void IncreaseTotalSum(double price)
        {
            TotalSum += price;
        }

        public void DecreaseTotalSum(double price)
        { 
            TotalSum -= price;
        }
    }
}
