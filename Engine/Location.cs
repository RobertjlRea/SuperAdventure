using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location

    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Item Itemrequiredtoenter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Mobs Monsterlivinghere { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        public Location(int id, string name, string desc, Item itemrequiredtoenter = null, 
                        Quest questavailableHere = null, Mobs monsterlivinghere= null)
        {
            ID = id;
            Name = name;
            Desc = desc;
            QuestAvailableHere = questavailableHere;
            Itemrequiredtoenter = itemrequiredtoenter;
            Monsterlivinghere = monsterlivinghere;
        }

    }
}
