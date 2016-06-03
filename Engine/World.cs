using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class World
    {
        #region Items
        // objects in game world stored here
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Mobs> _Mobs = new List<Mobs>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RAT_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR = 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_SPIDER_FANG = 8;
        public const int ITEM_ID_SPIDER_SILK = 9;
        public const int ITEM_ID_ADVENTURER_PASS = 10;
        public const int ITEM_ID_ORC_HEAD = 11; 
        public const int ITEM_ID_DAGGERFANG = 12;
        public const int ITEM_ID_TUNIC = 13;

        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_SPIDER = 3;
        public const int MONSTER_ID_ORC = 4;

        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMERS_FIELD = 1;

        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_TOWN_SQUARE = 2;
        public const int LOCATION_ID_GUARD_POST = 3;
        public const int LOCATION_ID_ALCHEMIST_HUT = 4;
        public const int LOCATION_ID_ALCHEMIST_GARDEN = 5;
        public const int LOCATION_ID_FARMHOUSE = 6;
        public const int LOCATION_ID_FARM_FIELD = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_SPIDER_FIELD = 9;
        public const int LOCATION_ID_ORC_CAVE = 10;
        #endregion

        static World()
        {
            PopulateItems();
            PopulateMobs();
            PopulateQuests();
            PopulateLocations();
        }
        #region Item Populate
        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5));
            Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rats tail", "Rat tails"));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur"));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake Fang", "Snake Fangs"));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins"));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "clubs", 3, 10));
            Items.Add(new HealingPots(ITEM_ID_HEALING_POTION, "Healing potion", "Healing Potions", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs"));
            Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider Silk", "Spider Silks"));
            Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer Pass", "Adventurer Passes"));
            Items.Add(new Item(ITEM_ID_RUSTY_SWORD, "Orc head", "Orc heads"));
        }
        private static void PopulateMobs()
        {
            Mobs rat = new Mobs(MONSTER_ID_RAT, "Rat", 5, 3, 10, 3, 3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

            Mobs Snake = new Mobs(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 3,5);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));

            Mobs Spider = new Mobs(MONSTER_ID_SPIDER, "Giant Spider", 20, 5, 40, 10, 10);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 25, true));

            Mobs Orc = new Mobs(MONSTER_ID_ORC, "Orc", 30, 10, 40, 10, 15);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ORC_HEAD), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_TUNIC), 25, true));

            _Mobs.Add(rat);
            _Mobs.Add(Snake);
            _Mobs.Add(Spider);
         }
        #endregion

        #region Quest populate

        private static void PopulateQuests()
        {
            Quest clearAlchemistGarden =
                new Quest(
                    QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                    "Clear the alchemist's garden",
                    "Help Ali the alchemist by killing the rats that have invested is garden, retrieve 3 rat tails",
                    20, 10);


            clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3));

            clearAlchemistGarden.rewarditem = ItemByID(ITEM_ID_HEALING_POTION);


            Quest clearFarmersField =
            new Quest(
                QUEST_ID_CLEAR_FARMERS_FIELD,
                "Clear the farmers field",
                "Terminate all the snakes so patrick can replant his vegetables and you will be rewarded", 20, 20);

            Quests.Add(clearAlchemistGarden);
            Quests.Add(clearFarmersField);
        }
        #endregion
        #region Locations
        private static void PopulateLocations()
        {

            //create Locations
            Location home = new Location(LOCATION_ID_HOME, "Home", "Your House is a complete tip");

            Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town Square", "You sit by a fountain");

            Location AlchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist Hut", "Colorful are with many strange smells and plants");
            AlchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMIST_GARDEN, "Alcemist Garden", 
                     "Many plants are growing here some destroyed by rats");
            alchemistsGarden.Monsterlivinghere = MonsterByID(MONSTER_ID_RAT);

            Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "A small farmhouse with a farmer on the balcony");
            farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

            Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmers Field",
                     "A wide open area, you can hear this hissing of snakes");
            farmersField.Monsterlivinghere = MonsterByID(MONSTER_ID_SNAKE);

            Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard Post", "There is a large looking guard here he resembles a troll",
                                 ItemByID(ITEM_ID_ADVENTURER_PASS));

            Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", " A stone bridge crosses a wide river, you hope there isnt a troll under");

            Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering the trees");
            spiderField.Monsterlivinghere = MonsterByID(MONSTER_ID_SPIDER);

            // Link locations together

            home.LocationToNorth = townSquare;

            townSquare.LocationToNorth = AlchemistHut;
            townSquare.LocationToSouth = home;
            townSquare.LocationToEast  = guardPost;
            townSquare.LocationToWest  = farmhouse;

            farmhouse.LocationToEast   = townSquare;
            farmhouse.LocationToWest   = farmersField;

            farmersField.LocationToEast = farmhouse;

            AlchemistHut.LocationToSouth = townSquare;
            AlchemistHut.LocationToNorth = alchemistsGarden;

            alchemistsGarden.LocationToSouth = AlchemistHut;

            guardPost.LocationToWest = bridge;
            guardPost.LocationToEast = townSquare;

            spiderField.LocationToEast = bridge;

            //add location to static list

            Locations.Add(home);
            Locations.Add(townSquare);
            Locations.Add(guardPost);
            Locations.Add(AlchemistHut);
            Locations.Add(alchemistsGarden);
            Locations.Add(farmhouse);
            Locations.Add(farmersField);
            Locations.Add(bridge);
            Locations.Add(spiderField);
        }   
        #endregion  

        public static Item ItemByID(int id)
        {
            foreach(Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;
                }
                     
            }
            return null;
        }


        public static Mobs MonsterByID(int id)
        {
            foreach (Mobs monster in _Mobs)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }

            return null;
        }

    }

}


