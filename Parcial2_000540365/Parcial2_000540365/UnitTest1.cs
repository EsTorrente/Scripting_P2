using NUnit.Framework;
using Parcial2_000540365.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_000540365
{
    [TestFixture]

    public class TestCreateItems
    {
        public static IEnumerable<TestCaseData> ItemData()
        {
            //válidos 
            yield return new TestCaseData("Sword", 100, ItemCategory.Weapon, true)
                .SetName("Valid_Weapon");

            yield return new TestCaseData("Armor", 50, ItemCategory.Armor, true)
                .SetName("Valid_Armor");

            yield return new TestCaseData("Ring", 25, ItemCategory.Accessory, true)
                .SetName("Valid_Accessory");

            yield return new TestCaseData("Potion", 10, ItemCategory.Supply, true)
                .SetName("Valid_Supply");

            //no fálidos
            yield return new TestCaseData("", 100, ItemCategory.Weapon, false)
                .SetName("Invalid_EmptyName");

            yield return new TestCaseData(null, 100, ItemCategory.Armor, false)
                .SetName("Invalid_NullName");

            yield return new TestCaseData("Sword", 0, ItemCategory.Accessory, false)
                .SetName("Invalid_ZeroPrice");

            yield return new TestCaseData("Sword", -10, ItemCategory.Supply, false)
                .SetName("Invalid_NegativePrice");
        }

        [TestCaseSource(nameof(ItemData))]
        public void TestItemValidation(string name, int price, ItemCategory category, bool expected)
        {
            var item = new Item(name, price, category);

            Assert.That(item.IsValid(), Is.EqualTo(expected));
        }
    }

    public class TestStore
    {
        public static IEnumerable<TestCaseData> StoreCreationData()
        {
            var validItem = new Item("Sword", 100, ItemCategory.Weapon);

            yield return new TestCaseData(validItem, 5, true)
                .SetName("ValidStoreCreation");

            yield return new TestCaseData(validItem, 0, false)
                .SetName("Invalid_ZeroQuantity");

            yield return new TestCaseData(null, 5, false)
                .SetName("Invalid_NullItem");
        }

        public static IEnumerable<TestCaseData> AddItemData()
        {
            var validItem = new Item("Sword", 100, ItemCategory.Weapon);
            var sameItemSamePrice = new Item("Sword", 100, ItemCategory.Weapon);
            var sameItemDifferentPrice = new Item("Sword", 200, ItemCategory.Weapon);
            var invalidItem = new Item("", 100, ItemCategory.Weapon);

            yield return new TestCaseData(validItem, 5, true)
                .SetName("Add_ValidItem");

            yield return new TestCaseData(validItem, -1, false)
                .SetName("Add_NegativeQuantity");

            yield return new TestCaseData(invalidItem, 5, false)
                .SetName("Add_InvalidItem");

            yield return new TestCaseData(sameItemSamePrice, 3, true)
                .SetName("Add_SameItemSamePrice");

            yield return new TestCaseData(sameItemDifferentPrice, 3, false)
                .SetName("Add_SameItemDifferentPrice");
        }

        public static IEnumerable<TestCaseData> IncreaseQuantityData()
        {
            yield return new TestCaseData(5, 3, 8)
                .SetName("Add_5_Then_3_Total_8");

            yield return new TestCaseData(1, 1, 2)
                .SetName("Add_1_Then_1_Total_2");

            yield return new TestCaseData(10, 5, 15)
                .SetName("Add_10_Then_5_Total_15");
        }

        // ==========================================================================================

        [TestCaseSource(nameof(StoreCreationData))]
        public void TestStoreCreation(Item item, int quantity, bool expected)
        {
            bool result;

            try
            {
                var store = new Store(item, quantity);
                result = store.Inventory.Count == 1;
            }
            catch //con el catch pq como el constructor tira exception, significa que incumplió con uno de los requisitos
            {
                result = false;
            }

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(AddItemData))]
        public void TestAddItem(Item item, int quantity, bool expected)
        {
            var baseItem = new Item("Sword", 100, ItemCategory.Weapon);
            var store = new Store(baseItem, 5);

            var result = store.AddItem(item, quantity);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(IncreaseQuantityData))]
        public void TestIncreasesQuantitySameItem(int firstQty, int secondQty, int expected)
        {
            var item = new Item("Sword", 100, ItemCategory.Weapon);
            var store = new Store(item, firstQty);

            store.AddItem(item, secondQty);

            Assert.That(store.Inventory[item], Is.EqualTo(expected));
        }
    }

    public class TestPlayer
    {
        public static IEnumerable<TestCaseData> PlayerCreationData()
        {
            yield return new TestCaseData(100, true)
                .SetName("Valid_Gold");

            yield return new TestCaseData(0, true)
                .SetName("Zero_Gold");

            yield return new TestCaseData(-10, false)
                .SetName("Negative_Gold");
        }

        public static IEnumerable<TestCaseData> SpendGoldData()
        {
            yield return new TestCaseData(100, 50, true, 50)
                .SetName("Spend_Valid");

            yield return new TestCaseData(100, 150, false, 100)
                .SetName("Spend_NotEnoughGold");

            yield return new TestCaseData(100, -10, false, 100)
                .SetName("Spend_InvalidAmount");
        }

        public static IEnumerable<TestCaseData> AddItemInventoryData()
        {
            yield return new TestCaseData(ItemCategory.Weapon, true, false)
                .SetName("Weapon_GoesToEquipment");

            yield return new TestCaseData(ItemCategory.Armor, true, false)
                .SetName("Armor_GoesToEquipment");

            yield return new TestCaseData(ItemCategory.Accessory, true, false)
                .SetName("Accessory_GoesToEquipment");

            yield return new TestCaseData(ItemCategory.Supply, false, true)
                .SetName("Supply_GoesToSupplies");
        }


        // ======================================================

        [TestCaseSource(nameof(PlayerCreationData))]
        public void TestPlayerCreation(int gold, bool expected)
        {
            var player = new Player(gold);

            Assert.That(player.CoinsAreValid(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(SpendGoldData))]
        public void TestSpendGold(int initialGold, int amount, bool expectedResult, int expectedGold)
        {
            var player = new Player(initialGold);

            var result = player.SpendGold(amount);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
        }

        [TestCaseSource(nameof(AddItemInventoryData))]
        public void TestAddItemToCorrectInventory(ItemCategory category, bool inEquipment, bool inSupplies)
        {
            var player = new Player(100);
            var item = new Item("TestItem", 10, category);

            player.AddItem(item, 1);

            Assert.That(player.Equipment.ContainsKey(item), Is.EqualTo(inEquipment));
            Assert.That(player.Supplies.ContainsKey(item), Is.EqualTo(inSupplies));
        }
    }

    public class TestBuy
    {
        public static IEnumerable<TestCaseData> BuyItemsData()
        {
            yield return new TestCaseData(
                50,//precio
                10, //stock tienda
                500,//gold jugador
                2,//cantidad a comprar
                true,
                400,//gold final
                8 //stock final
            ).SetName("Buy_Success");

            yield return new TestCaseData(
                100,
                10,
                50,
                1,
                false,
                50,
                10
            ).SetName("Buy_NotEnoughGold");

            yield return new TestCaseData(
                50,
                1,
                500,
                5, //más de lo disponible
                false,
                500,
                1
            ).SetName("Buy_NotEnoughStock");
        }

        [TestCaseSource(nameof(BuyItemsData))]
        public void TestBuyItems(
            int price,
            int storeStock,
            int playerGold,
            int quantity,
            bool expectedResult,
            int expectedGold,
            int expectedStock)
        {

            var item = new Item("Sword", price, ItemCategory.Weapon);
            var store = new Store(item, storeStock);
            var player = new Player(playerGold);

            var purchase = new Dictionary<Item, int>();
            purchase.Add(item, quantity);

            var result = store.BuyItems(player, purchase);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(player.Gold, Is.EqualTo(expectedGold));
            Assert.That(store.Inventory[item], Is.EqualTo(expectedStock));

            if (expectedResult)
                Assert.That(player.Equipment[item], Is.EqualTo(quantity));
        }
    }
}
