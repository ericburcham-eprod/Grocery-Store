﻿namespace GroceryStore
{
    public class LineItem
    {
        private readonly IProvideDeals _dealProvider;

        public LineItem(Item item, IProvideDeals dealProvider)
        {
            _dealProvider = dealProvider;
            Item = item;
            AddOne();
        }

        private Item Item { get; }

        public uint Quantity { get; private set; }

        public decimal RawTotal => Quantity * Item.Price;

        public string Sku => Item.Sku;
        public decimal Subtotal => RawTotal - Discount;
        public string Name => Item.Name;
        public decimal Discount => _dealProvider?.GetDiscount(Quantity, Item.Price) ?? 0;
        public decimal Price => Item.Price;

        public void AddOne()
        {
            Quantity += 1;
        }
    }
}