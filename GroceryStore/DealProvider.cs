using System.Collections.Generic;

namespace GroceryStore
{
    public class DealProvider : IProvideDeals
    {
        protected readonly Dictionary<string, IDeal> Deals;

        public DealProvider()
        {
            Deals = new Dictionary<string, IDeal>();
        }

        public IDeal GetDeal(string sku)
        {
            IDeal dealProvider;
            return Deals.TryGetValue(sku, out dealProvider) ? dealProvider : new DoNothingDeal();
        }
    }
}