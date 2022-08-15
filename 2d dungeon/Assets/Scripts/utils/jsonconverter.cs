using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Main
{
    public class jsonconverter : MonoBehaviour
    {
        public static void setjson(string path, string name, string set)
        {
            string json = File.ReadAllText(path);
            stored store = JsonUtility.FromJson<stored>(json);
            foreach (var val in store.store)
            {
                if (val.name.ToString() == name)
                {
                    val.value = set;
                    break;
                }
            }
            string jsonconvert = JsonUtility.ToJson(store);
            File.WriteAllText(path, jsonconvert);

        }
        public static string getjsonval(string path, string searchname)
        {
            string json = File.ReadAllText(path);
            stored store = JsonUtility.FromJson<stored>(json);
            foreach (var val in store.store)
            {
                if (val.name.ToString() == searchname)
                {
                    return val.value.ToString();
                }
            }
            return "";
        }
        public static KeyCode getjsonkeyval(string path, string searchname)
        {
            string json = File.ReadAllText(path);
            keystored store = JsonUtility.FromJson<keystored>(json);
            foreach (var val in store.store)
            {
                if (val.name.ToString() == searchname)
                {
                    return val.value;
                }
            }
            return KeyCode.None;
        }
        public class stored
        {
            public List<storing> store { get; set; }
        }
        public class storing
        {
            public string name;
            public string value;
        }
        public class keystored
        {
            public List<keystoring> store { get; set; }
        }
        public class keystoring
        {
            public string name;
            public KeyCode value;
        }

    }
}
