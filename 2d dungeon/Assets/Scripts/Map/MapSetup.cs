using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Main.RankStructure;

namespace Main
{

    public class Node
    {
        public float x, y;
        public GameObject MapObj;
        public bool Entity;

        public Vector2 GetActualLoc()
        {
            return new Vector2((0.16f * x) + 0.08f, (0.16f * y) + 0.08f);
        }
        public Node(float Xloc, float Yloc, GameObject Obj, bool entity)
        {
            x = Xloc;
            y = Yloc;
            MapObj = Obj;
            Entity = entity;
        }
        public Vector2 ToNodeLoc(Vector2 ActualLoc)
        {
            return new Vector2(Mathf.Round((ActualLoc.x - 0.08f) / 0.16f), Mathf.Round((ActualLoc.y - 0.08f) / 0.16f));
        }
    }
    public class MapSetup : MonoBehaviour
    {
        public int[,] Map = new int[1024, 1024];
        public Node[,] NodeMap = new Node[1024, 1024];
        public Vector2 SpawnLoc = Vector2.zero;
        public GameObject Floor;
        public GameObject Block;
        public GameObject SpawnPoint;
        public GameObject Player;
        public GameObject follower;
        public GameObject watcher;
        public GameObject AI;
        //int squadnum = 0;
        static public AIPlatoon platoon = new AIPlatoon("0", new List<AISquad>());
        static public AISquad squad = new AISquad("1", new List<AIModel>());
        //public GameObject Enemy;
        //public GameObject Door;
        // Start is called before the first frame update
        void Start()
        {
            Map = publicvariables.Map;

            int y1 = 0,x1 = 0;
            
            foreach(string s in File.ReadAllLines(Application.dataPath + "/Maps/Map.txt"))
            {
                foreach(string t in s.Split(','))
                {
                    Map[x1, y1] = int.Parse(t);
                    x1++;
                }
                y1++;
                x1 = 0;
            }

            platoon.Squads.Add(squad);
            
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    switch (Map[x, y])
                    {
                        case 1:
                            
                            
                            NodeMap[x, y] = new Node(x, y,Instantiate(Floor),false);
                            NodeMap[x, y].MapObj.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NodeMap[x, y].MapObj.GetComponent<SpriteRenderer>().enabled = true;
                            break;
                        case 2:
                            NodeMap[x, y] = new Node(x, y, Instantiate(Block),false);
                            NodeMap[x, y].MapObj.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NodeMap[x, y].MapObj.GetComponent<SpriteRenderer>().enabled = true;
                            break;
                        case 3:
                            NodeMap[x, y] = new Node(x, y, Instantiate(SpawnPoint),false);
                            NodeMap[x, y].MapObj.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NodeMap[x, y].MapObj.GetComponent<SpriteRenderer>().enabled = true;
                            SpawnLoc = new Vector2((16f * x) + 8f, (16f * y) + 8f);
                            break;
                        case 4:
                            NodeMap[x, y] = new Node(x, y, Instantiate(Floor),true);
                            NodeMap[x, y].MapObj.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NodeMap[x, y].MapObj.GetComponent<SpriteRenderer>().enabled = true;
                            GameObject NewAI = Instantiate(AI);
                            AIModel SQModel = new AIModel("AI" + x + y, 100, 30, 120, false, false, RoleType.SQUADDIE);
                            NewAI.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NewAI.GetComponent<SpriteRenderer>().enabled = true;
                            NewAI.GetComponent<AIDecision>().enabled = true;
                            NewAI.GetComponent<AIDecision>().AI = SQModel;
                            //platoon.Squads[platoon.Squads.Count - 1].Models.Add(SQModel);      an attempt to create a new squad for each commander however this causes issues with order of initialisation of commanders and squaddies
                            squad.Models.Add(NewAI.GetComponent<AIDecision>().AI);
                            Main.publicvariables.AllEnemy.Add(NewAI);
                            break;
                        case 5:
                            NodeMap[x, y] = new Node(x, y, Instantiate(Floor), true);
                            NodeMap[x, y].MapObj.transform.position = new Vector2((float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NodeMap[x, y].MapObj.GetComponent<SpriteRenderer>().enabled = true;
                            GameObject NewAIC = Instantiate(AI);
                            AIModel AICModel= new AIModel("AI" + x + y, 100, 30, 120, true, false, RoleType.COMMANDER);
                            NewAIC.transform.position = new Vector2( (float)Math.Round((16f * x) + 8f,2), (16f * y) + 8f);
                            NewAIC.GetComponent<SpriteRenderer>().enabled = true;
                            NewAIC.GetComponent<AIDecision>().enabled = true;
                            NewAIC.GetComponent<AIDecision>().AI = AICModel;
                            squad.Models.Add(NewAIC.GetComponent<AIDecision>().AI);
                            //AISquad NewSquad = new AISquad(squadnum.ToString(), new List<AIModel>);   lookabove, continued attempt to make a new squad
                            //NewSquad.Models.Add(AICModel);
                            //platoon.Squads.Add(NewSquad);
                            Main.publicvariables.AllEnemy.Add(NewAIC);
                            break;

                    }
                }
            }
            Player.transform.position = SpawnLoc;
            follower.transform.position = SpawnLoc;
            watcher.transform.position = new Vector3(SpawnLoc.x, SpawnLoc.y, -10) ;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
