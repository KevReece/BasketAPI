using System;
using System.Collections.Generic;
using System.Linq;
using BasketApi.Models;
using BasketApi.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasketApi.Tests.UnitTests.Services
{
    [TestClass]
    public class BasketServiceTests
    {
        private BasketService basketService;

        [TestInitialize]
        public void BeforeAllTests()
        {
            basketService = new BasketService();
        }

        [TestMethod]
        public void CreatesNewBasket()
        {
            var newBasket = basketService.Create();

            newBasket.Id.Should().NotBeNullOrWhiteSpace();
            newBasket.Items.Should().BeEmpty();
        }

        [TestMethod]
        public void CreatesMultipleBaskets()
        {
            var existingBasketA = basketService.Create();
            var existingBasketB = basketService.Create();

            existingBasketA.Id.Should().NotBe(existingBasketB.Id);
        }

        [TestMethod]
        public void CreatesNewBasketWithNewGuidId()
        {
            var newBasket = basketService.Create();

            new Guid(newBasket.Id).Should().NotBeEmpty();
        }

        [TestMethod]
        public void CreatesNewBasketWithAlphanumericLowercaseFormatId()
        {
            var newBasket = basketService.Create();

            newBasket.Id.Should().NotMatchRegex("[^a-z0-9]");
        }

        [TestMethod]
        public void GetsExistingBasket()
        {
            var existingBasket = basketService.Create();

            var basket = basketService.Get(existingBasket.Id);

            basket.Should().Be(existingBasket);
        }

        [TestMethod]
        public void GetsNullNonExistingBasket()
        {
            var basket = basketService.Get("unknown");

            basket.Should().BeNull();
        }

        [TestMethod]
        public void RetrievesBasketFromMultiple()
        {
            basketService.Create();
            var existingBasket = basketService.Create();
            basketService.Create();

            var basket = basketService.Get(existingBasket.Id);

            basket.Should().Be(existingBasket);
        }

        [TestMethod]
        public void RetrievesBasketInNewInstance()
        {
            var existingBasket = basketService.Create();

            var basket = new BasketService().Get(existingBasket.Id);

            basket.Should().Be(existingBasket);
        }

        [TestMethod]
        public void AddsItemToBasket()
        {
            var existingBasket = basketService.Create();
            var item = new Item{ Id = "ItemId", Quantity = 5};

            basketService.AddItem(existingBasket.Id, item.Id, item);

            var basketItems = basketService.Get(existingBasket.Id).Items;
            basketItems.Keys.Single().Should().Be(item.Id);
            basketItems[item.Id].Id.Should().Be(item.Id);
            basketItems[item.Id].Quantity.Should().Be(item.Quantity);
        }

        [TestMethod]
        public void AddItemOverwritesItemInBasket()
        {
            var existingBasket = basketService.Create();
            var item = SetupItemInBasket(existingBasket, "ItemId");
            item.Quantity = 100;

            basketService.AddItem(existingBasket.Id, item.Id, item);

            var basketItems = basketService.Get(existingBasket.Id).Items;
            basketItems.Keys.Single().Should().Be(item.Id);
            basketItems[item.Id].Quantity.Should().Be(100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemThrowsExceptionForInconsistentItemId()
        {
            var existingBasket = basketService.Create();
            var item = new Item { Id = "ItemId", Quantity = 5 };

            basketService.AddItem(existingBasket.Id, "ADifferentId", item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemThrowsExceptionForNullItemId()
        {
            var existingBasket = basketService.Create();
            var item = new Item { Id = null, Quantity = 5 };

            basketService.AddItem(existingBasket.Id, item.Id, item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemThrowsExceptionForBlankItemId()
        {
            var existingBasket = basketService.Create();
            var item = new Item { Id = string.Empty, Quantity = 5 };

            basketService.AddItem(existingBasket.Id, item.Id, item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemThrowsExceptionForUnknownBasket()
        {
            var item = new Item { Id = null, Quantity = 5 };

            basketService.AddItem("unknown", item.Id, item);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemThrowsExceptionForInvalidQuantity()
        {
            var existingBasket = basketService.Create();
            var item = new Item { Id = null, Quantity = 0 };

            basketService.AddItem(existingBasket.Id, item.Id, item);
        }

        [TestMethod]
        public void DeletesItemFromBasket()
        {
            var existingBasket = basketService.Create();
            var item = SetupItemInBasket(existingBasket, "ItemId");

            var deleted = basketService.DeleteItem(existingBasket.Id, item.Id);

            deleted.Should().BeTrue();
            var basketItems = basketService.Get(existingBasket.Id).Items;
            basketItems.Should().BeEmpty();
        }

        [TestMethod]
        public void DeletesItemReturnsFalseForUnknownBasket()
        {
            var deleted = basketService.DeleteItem("unknown", "unknown");

            deleted.Should().BeFalse();
        }

        [TestMethod]
        public void DeletesItemReturnsFalseForUnknownItem()
        {
            var existingBasket = basketService.Create();

            var deleted = basketService.DeleteItem(existingBasket.Id, "unknown");

            deleted.Should().BeFalse();
        }

        [TestMethod]
        public void DeletesItemFromBasketWithMultipleItems()
        {
            var existingBasket = basketService.Create();
            var item1 = SetupItemInBasket(existingBasket, "ItemId1");
            var item2 = SetupItemInBasket(existingBasket, "ItemId2");
            var item3 = SetupItemInBasket(existingBasket, "ItemId3");

            basketService.DeleteItem(existingBasket.Id, item2.Id);
            
            var basketItems = basketService.Get(existingBasket.Id).Items;
            basketItems.Should().NotContainKey(item2.Id);
            basketItems.Should().ContainKey(item1.Id);
            basketItems.Should().ContainKey(item3.Id);
        }

        [TestMethod]
        public void DeleteAllItems()
        {
            var existingBasket = basketService.Create();
            SetupItemInBasket(existingBasket, "ItemId");

            var deleted = basketService.DeleteAllItems(existingBasket.Id);

            deleted.Should().BeTrue();
            basketService.Get(existingBasket.Id).Items.Should().BeEmpty();
        }

        [TestMethod]
        public void DeleteAllItemseturnsFalseForUnknownBasket()
        {
            var deleted = basketService.DeleteAllItems("unknown");

            deleted.Should().BeFalse();
        }

        [TestMethod]
        public void DeleteAllItemsFromBasketWithMultipleItems()
        {
            var existingBasket = basketService.Create();
            SetupItemInBasket(existingBasket, "ItemId1");
            SetupItemInBasket(existingBasket, "ItemId2");

            basketService.DeleteAllItems(existingBasket.Id);

            basketService.Get(existingBasket.Id).Items.Should().BeEmpty();
        }

        private Item SetupItemInBasket(Basket existingBasket, string itemId)
        {
            var item = new Item {Id = itemId, Quantity = 5};
            basketService.AddItem(existingBasket.Id, item.Id, item);
            return item;
        }
    }
}