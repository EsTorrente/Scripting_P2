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

        public void AddItem(Item item, int quantity) { }

        public bool BuyItems(Player player, Dictionary<Item, int> items) 
        {
            return false; //temporal :P
        }
    }
}
