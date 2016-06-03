using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class HealingPots  : Item
    {
        
        public int AmountOfHeal { get; set; }

        public HealingPots(int id, string name, string nameplural, int amountofHeal) : base(id, name, nameplural)
        {
            AmountOfHeal = amountofHeal;

        }

    }
}
