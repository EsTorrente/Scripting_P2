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
            Equipment = new Dictionary<Item, int>();
            Supplies = new Dictionary<Item, int>();
        }

        public bool CoinsAreValid()
        {
            return false;
        }

        public bool CanAfford(int amount)
        {
            return false;
        }

        public void AddGold(int amount)
        {
            
        }
    }
}
