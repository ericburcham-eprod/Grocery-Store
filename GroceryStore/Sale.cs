﻿using System.Collections.Generic;
using System.Linq;

namespace GroceryStore
{
    public class Sale
    {
        public Sale()
        {
            LineItems = new List<LineItems>();
        }

        public IList<LineItems> LineItems { get; }

        public decimal Total => LineItems.Sum(item => item.Subtotal);

        public void AddItem(string sku)
        {
            var existingItem = LineItems.SingleOrDefault(lineItem => lineItem.Item.Sku == sku);

            if (existingItem != null)
            {
                existingItem.AddOne();
            }
            else
            {
                var item = ItemBuilder.BuildItem(sku);
                var lineItem = new LineItems(item);
                LineItems.Add(lineItem);
            }
        }
    }
}