using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LootItem
    {
        public Item Details {get; set;}
        public int DropPercentage { get; set; }
        public bool IsDefaultitem { get; set; }
        public List<LootItem> LootTable { get; set; }

        public LootItem(Item details, int dropPercentage, bool isdefaultitem)
        {
            Details = details;
            DropPercentage = dropPercentage;
            IsDefaultitem = isdefaultitem;
            LootTable = new List<LootItem>();

        }
    }
}
