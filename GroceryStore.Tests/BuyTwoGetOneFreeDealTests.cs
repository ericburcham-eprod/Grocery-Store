﻿using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using NUnit.Framework;

namespace GroceryStore.Tests
{
    [TestFixture]
    public class BuyTwoGetOneFreeDealTests
    {
        private readonly decimal[] _prices = { 1.25M, 10M, 4.88M };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GetDiscountReturnsZeroWhenQuantityLessThanThree()
        {
            var buyTwoGetOneFreeDeal = new BuyTwoGetOneFreeDeal();

            Parallel.For(
                0,
                3,
                i =>
                {
                    var quantity = (uint)i;
                    foreach (var price in _prices)
                    {
                        buyTwoGetOneFreeDeal.GetDiscount(quantity, price).Should().Be(0);
                    }
                });
        }

        [Test]
        public void GetDiscountReturnsQuantityDividedByThreeTimesPriceWhenQuantityIsDivisibleByThree()
        {
            var buyTwoGetOneFreeDeal = new BuyTwoGetOneFreeDeal();

            // We're only testing numbers divisible by three
            var quantities = Enumerable.Range(0, 1000).Where(i => i % 3 == 0).ToList();

            Parallel.ForEach(quantities,
                i =>
                {
                    var quantity = (uint)i;
                    var freeQuantity = quantity / 3;

                    foreach (var price in _prices)
                    {
                        buyTwoGetOneFreeDeal.GetDiscount(quantity, price).Should().Be(freeQuantity * price);
                    }
                });

        }

        [Test]
        public void BuyTwoGetOneFreeDealWithOddQuantityOfMoreThanTwoReturnsNextValueUnderQuantityDivisibleByThreeMinusOneDividedByTwoTimesPrice()
        {
            // Applying a buy 2 get 1 free deal when quantity is > 3 and quantity is not divisible by three returns ((quantity – 1) / 3) * price
            var buyTwoGetOneFreeDeal = new BuyTwoGetOneFreeDeal();

            // We're only testing numbers not divisible by three
            var quantities = Enumerable.Range(0, 1000).Where(i => i % 3 != 0).ToList();

            Parallel.ForEach(
                quantities,
                i =>
                {
                    var quantity = (uint)i;
                    var workingQuantity = quantity;
                    while (workingQuantity % 3 != 0)
                    {
                        workingQuantity -= 1;
                    }

                    var freeQuantity = workingQuantity / 3;

                    foreach (var price in _prices)
                    {
                        var expectedDiscount = freeQuantity * price;
                        var actualDiscount = buyTwoGetOneFreeDeal.GetDiscount(quantity, price);

                        actualDiscount.Should().Be(expectedDiscount);
                    }
                });
        }
    }
}
