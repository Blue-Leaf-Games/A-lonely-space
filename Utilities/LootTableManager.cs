using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Main
{
    public class LootTableManager
    {
        private Dictionary<string, LootTable> tables;
        private string path, extention;
        private Random random;

        public LootTableManager(string tablesPath, string ext, int seed) {
            path = tablesPath; extention = ext;
            random = seed == -1 ? new Random() : new Random(seed);
            loadLootTables();
        }

        public void loadLootTables() {
            Dictionary<string, LootTable> temp = new Dictionary<string, LootTable>();
            foreach (string f in Directory.GetFiles(path, extention))
            {
                LootTable tempTable = JsonConvert.DeserializeObject<LootTable>(File.ReadAllText(f));
                temp.Add(tempTable.name, tempTable);
            }
            tables = temp;
        }

        public ReturnItem[] rollTable(string tableName) {
            List<ReturnItem> temp = new List<ReturnItem>();
            LootTable table = tables[tableName];

            foreach(Pool p in table.pools) {
                if(p.rolls == -1) { //Use all entries
                    foreach(Entry e in p.entries) {
                        if (random.Next(0, 101) > e.chance) continue;
                        temp.Add(new ReturnItem { 
                            itemId = e.itemId, 
                            quantity = e.quantityMax == -1 ? e.quantityMin : random.Next(e.quantityMin, e.quantityMax+1) 
                        });
                    }
                }
                else { //Randomly use entries dependent on rolls
                    for (int i = 0; i < p.rolls; i++) {
                        Entry tempEntry = p.entries[random.Next(0, p.entries.Length)];
                        if (random.Next(0, 101) > tempEntry.chance) continue;
                        temp.Add(new ReturnItem { 
                            itemId = tempEntry.itemId, 
                            quantity = tempEntry.quantityMax == -1 ? tempEntry.quantityMin : random.Next(tempEntry.quantityMin, tempEntry.quantityMax + 1)
                        });
                    }
                }
            }
            return temp.ToArray();
        }
    }

    struct LootTable
    {
        public string name;
        public Pool[] pools;
    }

    struct Pool
    {
        public int rolls;
        public Entry[] entries;
    }

    struct Entry
    {
        public int itemId,
            quantityMin,
            quantityMax,
            chance;
    }

    public struct ReturnItem {
        public int itemId, quantity;
    }
}
