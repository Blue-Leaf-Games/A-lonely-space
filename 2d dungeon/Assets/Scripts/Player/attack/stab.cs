using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class stab : MonoBehaviour
    {
        public KeyCode stabAtt;
        public Rigidbody2D player;
        public float stabLength;
        public float stabspeed;
        public float endX;
        public float endY;
        public float constZ;
        public float rotationOffset;
        public bool stabbing = false;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            constZ = transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (stabbing)
            {
                Vector3.MoveTowards(player.transform.position, new Vector3(endX, endY, constZ), stabspeed * Time.deltaTime);
            }
            if (Input.GetKeyDown(stabAtt)&& !publicvariables.playerattacking && !publicvariables.playermoving)
            {
                publicvariables.playerattacking = true;
                GetComponent<Transform>().position = player.transform.position;
                switch(publicvariables.playerdirection)
                {
                    case 1:
                        endX = player.transform.position.x;
                        endY = player.transform.position.y + stabLength;
                        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90+rotationOffset);
                        break;
                    case -1:
                        endX = player.transform.position.x;
                        endY = player.transform.position.y - stabLength;
                        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + rotationOffset);
                        break;

                    case 2:
                        endX = player.transform.position.x + stabLength;
                        endY = player.transform.position.y;
                        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0 + rotationOffset);
                        break;
                    case -2:
                        endX = player.transform.position.x - stabLength;
                        endY = player.transform.position.y;
                        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 + rotationOffset);
                        break;
                }
                GetComponent<PolygonCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;


            }
            else if (new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y) == new Vector2(player.position.x, player.position.y) && publicvariables.playerattacking)
            {
                publicvariables.playerattacking=false;
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y) == new Vector2(endX,endY) && publicvariables.playerattacking )
            {
                endX = player.position.x;
                endY = player.position.y;
            }
            

        }
    }
}
