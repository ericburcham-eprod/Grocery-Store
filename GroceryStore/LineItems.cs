﻿namespace GroceryStore
{
    public class LineItems
    {
        public LineItems(Item item)
        {
            Item = item;
            AddOne();
        }

        public Item Item { get; }

        public int Quantity { get; private set; }

        public decimal Subtotal => Quantity * Item.Price;

        public void AddOne()
        {
            Quantity += 1;
        }
    }
}