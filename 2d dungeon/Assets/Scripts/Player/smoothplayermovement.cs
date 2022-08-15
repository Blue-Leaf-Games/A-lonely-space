
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public class smoothplayermovement : MonoBehaviour
    {
        public KeyCode keyup;
        public KeyCode keydown;
        public KeyCode keyleft;
        public KeyCode keyright;
        public Rigidbody2D player;
        public float speed;
        public Rigidbody2D cam;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //KeyBindManager keybinds = new KeyBindManager("2d-sci-fi-dungeon/Jsons/keybinds.json");
            if (Input.GetKey(keyup)&& !publicvariables.playerattacking)
            {
                player.velocity = new Vector2(player.velocity.x, speed );
            }
            
            else if (Input.GetKey(keydown)&& !publicvariables.playerattacking)
            {
                player.velocity = new Vector2(player.velocity.x, -speed) ;
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
            }
            if (Input.GetKey(keyleft)&& !publicvariables.playerattacking)
            {
                player.velocity=new Vector2(-speed , player.velocity.y);
            }
            else if (Input.GetKey(keyright)&&  !publicvariables.playerattacking)
            {
                player.velocity=new Vector2(speed, player.velocity.y);
               
            }
            else
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }
            //TODO play movement animation for each movement
            //TODO call public turnmade method
            cam.MovePosition(player.position);
        }

 
    }
}
