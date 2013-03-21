namespace CheckoutKataAttemptTwo
{
    public class Item
    {
        public string Name { get; private set; }

        public Item(string itemName)
        {
            Name = itemName;
        }
    }
}