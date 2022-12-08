using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main 
{
    
    public class publicvariables : MonoBehaviour
    {
        static public bool playerattacking = false;
        static public bool dockingportopen = true;
        static public bool playermoving = false;
        static public int playerdirection = -1;
        static public List<string> lastScene = new List<string>();
        static public int[,] Map = new int[1024, 1024];

        public static int listlength(List<string> listtemp)
        {
            int ret = 0;
            foreach(var sc in listtemp)
            {
                ret++;
            }
            return ret;
        }
    }
}
