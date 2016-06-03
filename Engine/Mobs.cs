using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Mobs : LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxDamage { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public List<LootItem> LootTable { get; set; }
        
        public Mobs(int id, string name, int maxdamage, int rewardgold, int rewardexp, int currenthitpoints, int maxhitpoints) : base(currenthitpoints, maxhitpoints)
        {

            ID = id;
            Name = name;
            MaxDamage = maxdamage;
            RewardGold = rewardgold;
            RewardExp = rewardexp;

            LootTable = new List<LootItem>();

            
        }
    }
}
