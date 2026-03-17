using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_000540365.Clases
{
    public class Item
    {
        public string Name { get; }
        public int Price { get; }
        public ItemCategory Category { get; }

        public Item(string name, int price, ItemCategory category)
        {
            Name = name;
            Price = price;
            Category = category;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            if (Price <= 0)
                return false;

            return true;
        }
    }
}
