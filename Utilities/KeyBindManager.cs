using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Main
{
    public class KeyBindManager
    {
        public Dictionary<string, int> keyBinds;
        private string path;

        public KeyBindManager(string filePath) {
            path = filePath;
            loadBinds();
        }

        private void loadBinds() {
            keyBinds = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(path));
        }

        private void saveBinds() {
            File.WriteAllText(path, JsonConvert.SerializeObject(keyBinds, Formatting.Indented));
        }

        public void changeBind(string key, int keyCode) {
            keyBinds[key] = keyCode;
            saveBinds();
        }
    }
}
