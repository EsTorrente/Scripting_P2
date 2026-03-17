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

        public Store(Item item, int quantity)
        {
            if (item == null)
                throw new Exception("Must have AT LEAST!!!!! one item to sell.");

            if (quantity <= 0)
                throw new Exception("Quantity must be greater than 0.");

            Inventory = new Dictionary<Item, int>();
            Inventory.Add(item, quantity);
        }

        public bool AddItem(Item item, int quantity)
        {
            if (item == null || !item.IsValid())
                return false;

            if (quantity <= 0)
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
            if (player == null || items == null || items.Count == 0)
                return false;

            int totalCost = 0;

            foreach (var pair in items)
            {
                var item = pair.Key;
                var quantity = pair.Value;

                if (item == null || quantity <= 0) //si no es válido el elemento
                    return false;

                if (!Inventory.ContainsKey(item)) //si no hay del objeto
                    return false;

                if (Inventory[item] < quantity) //si hay menos cantidad dispo que la que piden
                    return false;

                totalCost += item.Price * quantity;
            }

            if (!player.CanAfford(totalCost)) //si no tiene plata
                return false;

            //me dan miedo los foreach
            foreach (var pair in items)
            {
                var item = pair.Key;
                var quantity = pair.Value;

                Inventory[item] -= quantity;
                player.AddItem(item, quantity);
            }

            player.SpendGold(totalCost);

            return true;
        }

        public bool HasItems() //pensé que ya la había puesto en el primer commit, perdón :( tengo déficit de atención
        {
            return Inventory.Any(i => i.Value > 0);
        }
    }
}
