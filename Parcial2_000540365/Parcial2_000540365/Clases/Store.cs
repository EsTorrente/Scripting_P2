using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_000540365.Clases
{
    public class Store
    {
        public Dictionary<Item, int> Inventory { get; private set; }

        public Store()
        {
            Inventory = new Dictionary<Item, int>();
        }

        public bool AddItem(Item item, int quantity)
        {
            if (item == null || !item.IsValid())
                return false;

            if (quantity < 0)
                return false;

            //el cosito de que mismo nombre y categoría no pueden tener distintos precios
            var existing = Inventory.Keys.FirstOrDefault(i =>
                i.Name == item.Name && i.Category == item.Category);

            if (existing != null)
            {
                if (existing.Price != item.Price)
                    return false;

                Inventory[existing] += quantity;
                return true;
            }

            Inventory[item] = quantity;
            return true;
        }

        public bool BuyItems(Player player, Dictionary<Item, int> items)
        {
            return false; //después lo pongo
        }
    }
}
