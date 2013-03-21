using System.Collections.Generic;
using NUnit.Framework;

namespace CheckoutKataAttemptTwo.Tests
{
    [TestFixture]
    public class CheckoutKataAttemptTwoTests : ICanDisplayTotalCost
    {
        private int _receiveTotal;

        [Test]
        public void Given_single_item_Then_correct_price_is_displayed()
        {
            var item = new Item("A");
            var fakePriceRepository = new FakePriceRepository();
            fakePriceRepository.Add(item, 50);
            var itemPriceTotaler = new Totaler(fakePriceRepository, this);
            var checkout = new Checkout(itemPriceTotaler, new DiscountCalculator(itemPriceTotaler,new FakeDealRepository()));
            checkout.Scan(item);
            Assert.That(_receiveTotal, Is.EqualTo(fakePriceRepository.Get(item)));
        }

        [Test]
        public void Given_multiple_items_are_scanned_Then_correct_price_is_displayed()
        {
            var itemA = new Item("A");
            var itemB = new Item("B");
            const int priceA = 50;
            const int priceB = 30;
            const int totalPrice = priceA + priceB;
            var fakePriceRepository = new FakePriceRepository();
            fakePriceRepository.Add(itemA, priceA);
            fakePriceRepository.Add(itemB, priceB);
            var itemPriceTotaler = new Totaler(fakePriceRepository, this);
            var checkout = new Checkout(itemPriceTotaler, new DiscountCalculator(itemPriceTotaler, new FakeDealRepository()));
            checkout.Scan(itemA);
            checkout.Scan(itemB);
            Assert.That(_receiveTotal, Is.EqualTo(totalPrice));
        }


        [Test]
        public void Given_3_items_of_a_Then_display_is_130()
        {
            const int expectedPrice = 130;
            const int priceA = 50;
            var itemA = new Item("A");

            var fakePriceRepository = new FakePriceRepository();
            fakePriceRepository.Add(itemA, priceA);
            var itemPriceTotaler = new Totaler(fakePriceRepository, this);
            var fakeDealRepository = new FakeDealRepository();
            fakeDealRepository.add(new SingleItemDeal("A",3,-20));
            var checkout = new Checkout(itemPriceTotaler, new DiscountCalculator(itemPriceTotaler, fakeDealRepository));
            checkout.Scan(itemA);
            checkout.Scan(itemA);
            checkout.Scan(itemA);
            Assert.That(_receiveTotal, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void Given_2_items_of_b_Then_display_is_45()
        {
            const int expectedPrice = 45;
            const int priceB = 30;
            var itemB = new Item("B");
            var fakePriceRepository = new FakePriceRepository();
            fakePriceRepository.Add(itemB, priceB);
            var itemPriceTotaler = new Totaler(fakePriceRepository, this);
            var fakeDealRepository = new FakeDealRepository();
            fakeDealRepository.add(new SingleItemDeal("B",2,-15));
            var checkout = new Checkout(itemPriceTotaler, new DiscountCalculator(itemPriceTotaler, fakeDealRepository));
            checkout.Scan(itemB);
            checkout.Scan(itemB);
            Assert.That(_receiveTotal, Is.EqualTo(expectedPrice));
        }

        //[Test]
        //public void Test_Bob()
        //{
        //    const int expectedPrice = 260;
        //    const int priceA = 50;
        //    var itemA = new Item("A");

        //    var fakePriceRepository = new FakePriceRepository();
        //    fakePriceRepository.Add(itemA, priceA);
        //    var itemPriceTotaler = new Totaler(fakePriceRepository, this);
        //    var fakeDealRepository = new FakeDealRepository();
        //    fakeDealRepository.add(new SingleItemDeal("A", 3, -20));
        //    var checkout = new Checkout(itemPriceTotaler, new DiscountCalculator(itemPriceTotaler, fakeDealRepository));
        //    checkout.Scan(itemA);
        //    checkout.Scan(itemA);
        //    checkout.Scan(itemA);
        //    checkout.Scan(itemA);
        //    checkout.Scan(itemA);
        //    checkout.Scan(itemA);
        //    Assert.That(_receiveTotal, Is.EqualTo(expectedPrice));
        //}

        public void Display(int total)
        {
            _receiveTotal = total;
        }


        internal class FakeDealRepository : ICanGetSingleItemDeals
        {
            private readonly List<SingleItemDeal> deals = new List<SingleItemDeal>();
            
            public void add(SingleItemDeal deal)
            {
                deals.Add(deal);
            }
            public ISingleItemDeal Get(string itemName)
            {
                ISingleItemDeal returner = new NullSingleItemDeal();
                foreach (var deal in deals)
                {
                    
                    if (deal.Name == itemName)
                    {
                        returner = deal;
                    }
                }

                return returner;
            }

        }

        internal class FakePriceRepository : ICanGetPrices
        {
            private readonly Dictionary<Item, int> _itemPriceLookup = new Dictionary<Item, int>();  
            public int Get(Item item)
            {
                return _itemPriceLookup[item];
            }

            public void Add(Item item, int price)
            {
                _itemPriceLookup.Add(item, price);
            }
        }
    }
}