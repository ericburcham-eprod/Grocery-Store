﻿using GroceryStore.Domain;

namespace GroceryStore
{
    public interface IBuildItems
    {
        Item BuildItem(string sku);
    }
}