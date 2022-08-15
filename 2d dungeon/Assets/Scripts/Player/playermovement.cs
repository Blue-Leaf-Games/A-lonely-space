
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public class playermovement : MonoBehaviour
    {
        public KeyCode keyup;
        public KeyCode keydown;
        public KeyCode keyleft;
        public KeyCode keyright;
        public Rigidbody2D player;
        public float tilesize;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //KeyBindManager keybinds = new KeyBindManager("2d-sci-fi-dungeon/Jsons/keybinds.json");
            if (Input.GetKeyDown(keyup)&& isspriteatloc(new Vector2(player.position.x, player.position.y + tilesize))&& !publicvariables.playerattacking)
            {
                player.MovePosition(new Vector2(player.position.x, player.position.y + tilesize));
            }
            if (Input.GetKeyDown(keydown)&& isspriteatloc(new Vector2(player.position.x, player.position.y - tilesize))&& !publicvariables.playerattacking)
            {
                player.MovePosition(new Vector2(player.position.x, player.position.y - tilesize));
            }
            if (Input.GetKeyDown(keyleft)&& isspriteatloc(new Vector2(player.position.x - tilesize, player.position.y))&& !publicvariables.playerattacking)
            {
                player.MovePosition(new Vector2(player.position.x - tilesize, player.position.y));
            }
            if (Input.GetKeyDown(keyright)&& isspriteatloc(new Vector2(player.position.x + tilesize, player.position.y)) && !publicvariables.playerattacking)
            {
                player.MovePosition(new Vector2(player.position.x + tilesize, player.position.y));
            }
            //TODO play movement animation for each movement
            //TODO call public turnmade method
        }

        private bool isspriteatloc(Vector2 loc)
        {
            GameObject[] spritelist = GameObject.FindGameObjectsWithTag("scenery");
            foreach (GameObject o in spritelist)
            {
                if (o.GetComponent<Renderer>().isVisible == true)
                {
                    if (o.transform.position.x == loc.x && o.transform.position.y == loc.y)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
