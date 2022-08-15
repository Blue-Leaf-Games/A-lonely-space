using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockyMovement : MonoBehaviour
{
    public bool moving = false;
    public Rigidbody2D rb;
    public float animationspeed;
    public float dist;
    public float initx;
    public float inity;
    public float endx;
    public float endy;
    public bool rbcollision;
    public LayerMask colliders;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(checkloc());
        //if (moving && rbcollision)
        //{
        //    rb.MovePosition(new Vector2(initx, inity));
        //    rb.velocity = Vector2.zero;
        //    moving = false;
        //}
        //KeyCode up = jsonconverter.getjsonkeyval(Application.dataPath + "/Scripts/jsons/keybinds.json", "up");
        //KeyCode down = jsonconverter.getjsonkeyval(Application.dataPath + "/Scripts/jsons/keybinds.json", "down");
        //KeyCode left = jsonconverter.getjsonkeyval(Application.dataPath + "/Scripts/jsons/keybinds.json", "left");
        //KeyCode right = jsonconverter.getjsonkeyval(Application.dataPath + "/Scripts/jsons/keybinds.json", "right");
        KeyCode up = KeyCode.W;
        KeyCode down = KeyCode.S;
        KeyCode left = KeyCode.A;
        KeyCode right = KeyCode.D;
        if (Vector2.Distance(rb.position, new Vector2(endx, endy)) ==0)
        {
            if (Input.GetKey(up) && !publicvariables.playerattacking && !moving && !Physics2D.OverlapBox(new Vector2(rb.transform.position.x + dist, rb.transform.position.y), new Vector2(dist, dist), 0,1))
            {
                //moving = true;
                initx = rb.transform.position.x;
                inity = rb.transform.position.y;
                endy = inity + dist;
                endx = initx;
                rb.MovePosition(new Vector3(endx, endy, animationspeed * Time.deltaTime));
                //rb.velocity = new Vector2(0, animationspeed);
            }
            else if (Input.GetKey(down) && !publicvariables.playerattacking && !moving && !Physics2D.OverlapBox(new Vector2(rb.transform.position.x - dist, rb.transform.position.y), new Vector2(dist, dist), 0,1))
            {
                //moving = true;
                initx = rb.transform.position.x;
                inity = rb.transform.position.y;
                endy = inity - dist;
                endx = initx;
                rb.MovePosition(new Vector3(endx, endy, animationspeed * Time.deltaTime));
                //rb.velocity = new Vector2(0, -animationspeed);
            }
            else if (Input.GetKey(left) && !publicvariables.playerattacking && !moving && !Physics2D.OverlapBox(new Vector2(rb.transform.position.x, rb.transform.position.y - dist), new Vector2(dist,dist), 0,1))
            {
                //moving = true;
                initx = rb.transform.position.x;
                inity = rb.transform.position.y;
                endx = initx - dist;
                endy = inity;
                rb.MovePosition(new Vector3(endx, endy, animationspeed * Time.deltaTime));
                //rb.velocity = new Vector2(-animationspeed, 0);
            }
            else if (Input.GetKey(right) && !publicvariables.playerattacking && !moving && !Physics2D.OverlapBox(new Vector2(rb.transform.position.x, rb.transform.position.y + dist), new Vector2(dist, dist), 0,1))
            {
                //moving = true;
                initx = rb.transform.position.x;
                inity = rb.transform.position.y;
                endx = initx + dist;
                endy = inity;
                rb.MovePosition(new Vector3(endx, endy, animationspeed * Time.deltaTime));
                //rb.velocity = new Vector2(animationspeed, 0);
            }
        }

    }
    IEnumerator checkloc()
        {
        double halfrange = 0.00005 * animationspeed * Time.deltaTime;
        if (rb.transform.position.x >= endx - halfrange && rb.transform.position.x <= endx + halfrange && rb.transform.position.y >= endy - halfrange && rb.transform.position.y <= endy + halfrange && moving)
        {
            
            rb.velocity = Vector2.zero;
            rb.MovePosition(new Vector2(endx, endy));
            yield return new WaitForSeconds(Time.deltaTime);
            moving = false;

        }
    }
}
