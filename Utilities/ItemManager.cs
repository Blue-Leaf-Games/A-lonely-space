using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Main
{
    public class ItemManager
    {
        private Item[] items;
        private string filePath;

        public ItemManager(string path) {
            filePath = path;
            loadItems();
        }

        private void loadItems() {
            items = JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(filePath));
        }

        public Item getItem(int id) {
            return items[id];
        }
    }

    public struct Item {
        public int id, type, value;
        public string name;
        public Modifier[] modifiers;
    }

    public struct Modifier {
        public int stat, modifier, duration, target;
    }
}
