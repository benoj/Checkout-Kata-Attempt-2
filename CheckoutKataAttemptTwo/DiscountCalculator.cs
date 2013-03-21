using System.Collections.Generic;
using CheckoutKataAttemptTwo.Tests;

namespace CheckoutKataAttemptTwo
{
    public class DiscountCalculator
    {
        private readonly Dictionary<string, int> _basket = new Dictionary<string, int>();
        private readonly ICanTellDisplayTheTotal _totaler;
        private readonly ICanGetSingleItemDeals _discountRepository;

        public DiscountCalculator(ICanTellDisplayTheTotal totaler, ICanGetSingleItemDeals discountRepository)
        {
            _totaler = totaler;
            _discountRepository = discountRepository;
        }

        public void AddItem(Item item)
        {
            if(_basket.ContainsKey(item.Name))
            {
                _basket[item.Name]++;
            }
            else
            {
                _basket.Add(item.Name,1);
            }
            var discount = calculateDiscount();
            _totaler.AddDiscount(discount);
        }

        private int calculateDiscount()
        {
            int discount = 0;
            
            foreach (var item in _basket)
            {
                var deal = _discountRepository.Get(item.Key);
                discount += (item.Value / deal.NumberToQualify) * deal.Discount;
              
            }

            return discount;
        }
    }
}