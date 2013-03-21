namespace CheckoutKataAttemptTwo
{
    public interface ISingleItemDeal
    {
        int NumberToQualify { get; }
        int Discount { get; set; }
        string Name { get; }
    }
    public class NullSingleItemDeal : ISingleItemDeal
    {
        public NullSingleItemDeal()
        {
            NumberToQualify = -1;
            Discount = 0;
            Name = string.Empty;
        }
        public int NumberToQualify { get; private set; }
        public int Discount { get; set; }
        public string Name { get; private set; }
    }

    public class SingleItemDeal : ISingleItemDeal
    {
        public int NumberToQualify { get; private set; }
        public int Discount { get; set; }
        public string Name { get; private set; }

        public SingleItemDeal(string itemName, int numberToQualify, int discount)
        {
            Name = itemName;
            NumberToQualify = numberToQualify;
            Discount = discount;
        }
    }
}