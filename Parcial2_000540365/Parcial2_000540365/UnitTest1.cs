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
        public static IEnumerable<TestCaseData> AddItemData()
        {
            var validItem = new Item("Sword", 100, ItemCategory.Weapon);
            var sameItemSamePrice = new Item("Sword", 100, ItemCategory.Weapon);
            var sameItemDifferentPrice = new Item("Sword", 200, ItemCategory.Weapon);
            var invalidItem = new Item("", 100, ItemCategory.Weapon);

            //válido
            yield return new TestCaseData(validItem, 5, true)
                .SetName("Add_ValidItem");

            //cantidad negativa
            yield return new TestCaseData(validItem, -1, false)
                .SetName("Add_NegativeQuantity");

            //item inválido
            yield return new TestCaseData(invalidItem, 5, false)
                .SetName("Add_InvalidItem");

            //mismo item mismo precio
            yield return new TestCaseData(sameItemSamePrice, 3, true)
                .SetName("Add_SameItemSamePrice");

            //mismo item distinto precio
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

        [TestCaseSource(nameof(AddItemData))]
        public void TestAddItem(Item item, int quantity, bool expected)
        {
            var store = new Store();

            store.AddItem(new Item("Sword", 100, ItemCategory.Weapon), 5);

            var result = store.AddItem(item, quantity);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(IncreaseQuantityData))]
        public void TestIncreasesQuantitySameItem(int firstQty, int secondQty, int expected)
        {
            var store = new Store();
            var item = new Item("Sword", 100, ItemCategory.Weapon);

            store.AddItem(item, firstQty);
            store.AddItem(item, secondQty);

            Assert.That(store.Inventory[item], Is.EqualTo(expected));
        }
    }
}
