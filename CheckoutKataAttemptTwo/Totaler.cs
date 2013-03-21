namespace CheckoutKataAttemptTwo
{
    public class Totaler : ICanTellDisplayTheTotal
    {
        private readonly ICanGetPrices _priceRepository;
        private readonly ICanDisplayTotalCost _display;
        private int _subTotal;
        public Totaler(ICanGetPrices priceRepository, ICanDisplayTotalCost display)
        {
            _priceRepository = priceRepository;
            _display = display;
        }

        public void AddDiscount(int discount)
        {
            _subTotal += discount;
            _display.Display(_subTotal);
        }

        public void Add(Item item)
        {
            _subTotal += _priceRepository.Get(item);

            _display.Display(_subTotal);
        }
    }
}