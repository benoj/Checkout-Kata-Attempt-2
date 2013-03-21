namespace CheckoutKataAttemptTwo
{
    public class Checkout
    {
        private readonly ICanTellDisplayTheTotal _itemPriceTotaler;
        private DiscountCalculator _bob;

        public Checkout(ICanTellDisplayTheTotal itemPriceTotaler, DiscountCalculator discountCalculator)
        {
            _itemPriceTotaler = itemPriceTotaler;
            _bob = discountCalculator;
        }
           

        public void Scan(Item item)
        {
            _itemPriceTotaler.Add(item);
            _bob.AddItem(item);
        }
    }


}