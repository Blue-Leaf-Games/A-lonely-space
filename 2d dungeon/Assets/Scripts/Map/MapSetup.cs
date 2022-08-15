using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class MapSetup : MonoBehaviour
    {
        public int[,] Map = new int[1024, 1024];

        public Vector2 SpawnLoc = Vector2.zero;
        public GameObject floor;
        public GameObject Block;
        public GameObject SpawnPoint;
        public GameObject Enemy;
        public GameObject Door;
        // Start is called before the first frame update
        void Start()
        {
            Map = publicvariables.Map;
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    GameObject piece = new GameObject();
                    switch (Map[x, y])
                    {
                        case 0:
                            piece = floor;
                            break;
                        case 1:
                            piece = Block;
                            break;
                        case 2:
                            piece = SpawnPoint;
                            SpawnLoc = new Vector2((0.16f * x) + 0.08f, (0.16f * y) + 0.08f);
                            break;
                    }
                    piece.transform.position = new Vector2((0.16f * x) + 0.08f, (0.16f * y) + 0.08f);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
