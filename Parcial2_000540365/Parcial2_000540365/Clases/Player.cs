using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_000540365.Clases
{
    public class Player
    {
        public int Gold { get; private set; }

        public Dictionary<Item, int> Equipment { get; }
        public Dictionary<Item, int> Supplies { get; }

        public Player(int gold)
        {
            Gold = gold;
            Equipment = new Dictionary<Item, int>();
            Supplies = new Dictionary<Item, int>();
        }

        public bool CoinsAreValid()
        {
            return Gold >= 0;
        }

        public bool CanAfford(int amount)
        {
            return amount >= 0 && Gold >= amount;
        }

        public void AddGold(int amount)
        {
            if (amount > 0)
                Gold += amount;
        }

        public bool SpendGold(int amount) //también se me olvidó agregarlos al inicio :( perdón, estaba muy nerviosa y la ansiedad me distrajo mucho.
        {
            if (!CanAfford(amount))
                return false;

            Gold -= amount;
            return true;
        }

        public void AddItem(Item item, int quantity)
        {
            if (item == null || quantity <= 0)
                return;

            var target = item.Category == ItemCategory.Supply
                ? Supplies
                : Equipment;

            if (target.ContainsKey(item))
                target[item] += quantity;
            else
                target[item] = quantity;
        }
    }
}
