using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public string Desc { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set;}
        public Item rewarditem { get; set; }

        public Quest(int id, string name, string desc, int rewardExp, int rewardgold)
        {
            ID = id;
            Name = name;
            Desc = desc;
            RewardExp = rewardExp;
            RewardGold = rewardgold;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }

    }
}
