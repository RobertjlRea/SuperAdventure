using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreature
    {
        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get; set; }

        public LivingCreature(int currenthitpoints, int maxhitpoints)
        {
            CurrentHitPoints = currenthitpoints;
            MaxHitPoints = maxhitpoints;
        }
    }
}
