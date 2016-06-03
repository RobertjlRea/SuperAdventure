using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace SuperAdventure
{


    public partial class Superadventure : Form
    {
        private Player _player;
        private Mobs _currentmonster;

        public Superadventure()
        {
            InitializeComponent();

            #region PlayerStats
            _player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblExperience.Text = _player.Exp.ToString();
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblGold.Text = _player.Gold.ToString();
            #endregion

            Location location = new Location(1, "Home", "This is the place of your birth");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Superadventure_Load(object sender, EventArgs e)
        {

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            //does the location have any items required
            if (newLocation.Itemrequiredtoenter != null)
            {
                //See if the player has required item in the inventory
                bool playerHasRequiredItem = false;

                foreach (InventoryItem ii in _player.Inventory)
                {
                    if (ii.Details.ID == newLocation.Itemrequiredtoenter.ID)
                    {
                        playerHasRequiredItem = true;
                        break; //Exit out of the loop
                    }
                }
                if (!playerHasRequiredItem)
                {
                    rtbMessages.Text += "You must have a " + newLocation.Itemrequiredtoenter.Name + " to enter this location" +
                        Environment.NewLine;
                    return;
                }
            }


            //Update the players current location
            _player.CurrentLocation = newLocation;

            //show/hide aviable moves
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            //Display current Location name and Descriptions
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text = newLocation.Desc + Environment.NewLine;

            // Heal the player fully
            _player.CurrentHitPoints = _player.MaxHitPoints;

            // Update hit points
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            //Does the location have quest
             if (newLocation.QuestAvailableHere != null)
            {
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                // If the player has not completed the quest yet
                if (!playerAlreadyCompletedQuest)
                {
                    // See if the player has all the items needed to complete the quest
                    bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletedItems(newLocation.QuestAvailableHere);

                    // The player has all items required to complete the quest
                    if (playerHasAllItemsToCompleteQuest)
                    {
                        // Display message
                        rtbMessages.Text += Environment.NewLine;
                        rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                        // Remove quest items from inventory
                        _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                        // Give quest rewards
                        rtbMessages.Text += "You receive: " + Environment.NewLine;
                        rtbMessages.Text += newLocation.QuestAvailableHere.RewardExp.ToString() + " experience points" + Environment.NewLine;
                        rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                        rtbMessages.Text += newLocation.QuestAvailableHere.rewarditem.Name + Environment.NewLine;
                        rtbMessages.Text += Environment.NewLine;

                        _player.Exp += newLocation.QuestAvailableHere.RewardExp;
                        _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                        // Add the reward item to the player's inventory
                        _player.AddItemToInventory(newLocation.QuestAvailableHere.rewarditem);

                        // Mark the quest as completed
                        _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                    }
                }
            }
            
            else
            {
                // The player does not already have the quest

                // Display the messages
                rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                rtbMessages.Text += newLocation.QuestAvailableHere.Desc + Environment.NewLine;
                rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                {
                    if (qci.Quantity == 1)
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                    }
                }
                rtbMessages.Text += Environment.NewLine;

                // Add the quest to the player's quest list
                _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
            }

            //does the location have a monster?
            if (newLocation.Monsterlivinghere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.Monsterlivinghere.Name + Environment.NewLine;

                //Make a new Monster, using the values from the standard monster in world.Monster list
                Mobs standardMonster = World.MonsterByID(newLocation.Monsterlivinghere.ID);

                _currentmonster = new Mobs(standardMonster.ID, standardMonster.Name, standardMonster.MaxDamage,
                                           standardMonster.RewardExp, standardMonster.RewardGold, standardMonster.CurrentHitPoints,
                                           standardMonster.MaxHitPoints);

                foreach (LootItem lootitem in standardMonster.LootTable)
                {
                    _currentmonster.LootTable.Add(lootitem);

                }
                cboWeapon.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentmonster = null;

                cboWeapon.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;

            }

            //Refresh players inventory
            UpdateInventoryListInUI();

            //Update Quest List
            UpdateQuestListInUI();

            //UpdateWeaponListInUI;
            UpdateWeaponListInUI();

            //update potion list
            UpdatePotionListInUI();
        }

            private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[0].Name = "Quantity";

            dgvInventory.Rows.Clear();


            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }


            }
        }
        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                //the player doesnt have any weapons, so btn and combo box will be hiddent
                cboWeapon.Visible = false;
                btnUseWeapon.Visible = false;

            }
            else
            {
                cboWeapon.DataSource = weapons;
                cboWeapon.DisplayMember = "Name";
                cboWeapon.ValueMember = "ID";

                cboWeapon.SelectedIndex = 0;
            }
        }
        private void UpdatePotionListInUI()
        {
            // Refresh player's potions combobox
            List<HealingPots> healingPots = new List<HealingPots>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPots)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPots.Add((HealingPots)inventoryItem.Details);
                    }
                }
            }

            if (healingPots.Count == 0)
            {
                //The player has no pots
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPots;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            // Get currently selected weapon
            Weapon currentWeapon = (Weapon)cboWeapon.SelectedItem;

            //determines damage
            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinDamage, currentWeapon.MaxDamage);

            //apply damage
            _currentmonster.CurrentHitPoints -= damageToMonster;

            //disp attack
            rtbMessages.Text += "You hit the " + _currentmonster.Name + " for " + damageToMonster.ToString() + " points " + Environment.NewLine;

            // check if monster is dead

            if (_currentmonster.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentmonster.Name + Environment.NewLine;

                //give player exp and GP
                _player.Exp += _currentmonster.RewardExp;
                rtbMessages.Text += "You reieve" + _currentmonster.RewardExp.ToString() + " Exp" + Environment.NewLine;

                _player.Exp += _currentmonster.RewardExp;
                rtbMessages.Text += "You reieve" + _currentmonster.RewardGold.ToString() + " Gold" + Environment.NewLine;

                //get loot
                List<InventoryItem> LootedItems = new List<InventoryItem>();

                foreach (LootItem lootitem in _currentmonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootitem.DropPercentage)
                    {
                        LootedItems.Add(new InventoryItem(lootitem.Details, 1));
                    }
                }
                //if nothing was dropped
                if (LootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentmonster.LootTable)
                    {
                        if (lootItem.IsDefaultitem)
                        {
                            LootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }
                foreach (InventoryItem inventoryItem in LootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);
                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }
                // refresh player info
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.Exp.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();

                //Add a blank line
                rtbMessages.Text += Environment.NewLine;

                MoveTo(_player.CurrentLocation);
            }

            else
            {
                int damageToPLayer = RandomNumberGenerator.NumberBetween(0, _currentmonster.MaxDamage);

                rtbMessages.Text += "The " + _currentmonster.Name + " did " + damageToPLayer.ToString() + " points of damage. " + Environment.NewLine;

                _player.CurrentHitPoints -= damageToPLayer;

                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                if (_player.CurrentHitPoints <= 0)
                {
                    rtbMessages.Text += "The " + _currentmonster.Name + " Killed you." + Environment.NewLine;

                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));

                }
            }
        }
        
        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            HealingPots potion = (HealingPots)cboPotions.SelectedItem;

            _player.CurrentHitPoints = (_player.CurrentHitPoints + potion.AmountOfHeal);

            if(_player.CurrentHitPoints > _player.MaxHitPoints)
            {
                _player.CurrentHitPoints = _player.MaxHitPoints;
            }

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details.ID == potion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }

            //display message
            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;

            // Monster gets turn

            //determine amount of damage
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentmonster.MaxDamage);

            //Disp Message
            rtbMessages.Text += "The " + _currentmonster.Name + " did " + damageToPlayer.ToString() + " points of damage" + Environment.NewLine;

            //subtract damage
            _player.CurrentHitPoints -= damageToPlayer;

            if(_player.CurrentHitPoints <= 0)
            {
                //disp message
                rtbMessages.Text += "The " + _currentmonster.Name + " killed you." + Environment.NewLine;

                //move home
                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            }

            //refresh player data
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            UpdateInventoryListInUI();
            UpdatePotionListInUI();


        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

