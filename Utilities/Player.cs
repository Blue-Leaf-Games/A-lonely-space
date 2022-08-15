using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace GameUtilities
{
    class Player
    {
        private string filepath;
        private ItemManager itemManager;

        public int currentHealth;
        public List<Modifier> activeModifiers;
        public List<Item> inventory;
        public Item[] equipment; //equipped items (weapon, armour, acc1,2,3)

        public Player(string path, ItemManager iManager) {
            itemManager = iManager;
            filepath = path;
            setInitialData(JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(filepath)));
        }

        private void setInitialData(PlayerData dataIn) {
            currentHealth = dataIn.health;
            activeModifiers = dataIn.modifiers.ToList();
            inventory = new List<Item>();
            foreach (int id in dataIn.inv) inventory.Add(itemManager.getItem(id));
            List<Item> tempEquipment = new List<Item>();
            foreach (int id in dataIn.equipped) tempEquipment.Add(itemManager.getItem(id));
            equipment = tempEquipment.ToArray();
        }

        public void UpdateAll() { //runs every combat round
            foreach(Modifier m in activeModifiers) {
                if (m.duration == -1) continue;
                if (m.duration - 1 == 0)
                {
                    activeModifiers.Remove(m);
                }
                //todo decrease duration of all other
            }
        }

        private void updateModifiers() {

        }

        public void Save() {
            File.WriteAllText(filepath, JsonConvert.SerializeObject(new PlayerData() {
                health = currentHealth,
                modifiers = activeModifiers.ToArray(),
                inv = getInvIds(inventory.ToArray()), 
                equipped = getInvIds(equipment)
            }, Formatting.Indented));
        }

        private int[] getInvIds(Item[] vals) {
            List<int> invIds = new List<int>();
            foreach(Item i in vals) { invIds.Add(i.id); }
            return invIds.ToArray();
        }
    }
    public struct PlayerData {
        public int health;
        public Modifier[] modifiers;
        public int[] inv;
        public int[] equipped;
    }
}
