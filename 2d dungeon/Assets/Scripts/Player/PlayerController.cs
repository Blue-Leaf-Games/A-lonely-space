using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Main.RankStructure;

public class PlayerController : MonoBehaviour
{
    public float movespeed;
    public Transform movepoint;
    public float dist;
    public float constz;
    public LayerMask colliders;
    public float Health;
    public int WeaponAmmo;
    public int WeaponDamage;
    public int ReserveAmmo;
    public Rigidbody2D Bullet;
    public float ShotSpeed;
    public Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        movepoint.parent = null;
        constz = transform.position.z;
        colliders = LayerMask.GetMask("MapObjects", "EnemyAI");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, constz), new Vector3(movepoint.position.x,movepoint.position.y, constz), movespeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movepoint.position) == 0 && Main.publicvariables.playerattacking == false)
        {
            
            Main.publicvariables.playermoving = false;
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * dist, 0f, 0f), (float)Math.Round((dist/2f),2),colliders)) //TODO Fix 2.3f divider, should be 2f
                {
                    Main.publicvariables.playermoving = true;
                    movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * dist, 0f, 0f);
                    Main.publicvariables.playerdirection = 2*Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
                    BroadcastToEnemy("Action");
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * dist, 0f), (float)Math.Round((dist/2f),2),colliders))//TODO Fix 2.3f divider, should be 2f
                {
                    Main.publicvariables.playermoving = true;
                    movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * dist, 0f);
                    Main.publicvariables.playerdirection = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
                    BroadcastToEnemy("Action");
                }
            }
            if(Input.GetMouseButtonDown(0))
            {
                float RandAngle = UnityEngine.Random.Range(-5f, 5f);
                Rigidbody2D Shot = Instantiate(Bullet);
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Shot.GetComponent<Collider2D>(),true);
                Shot.position = this.transform.position;
                Shot.gameObject.transform.Rotate(new Vector3(0, 0, LinearAngle(Cam.ScreenToWorldPoint(Input.mousePosition), transform.position) + RandAngle));
                Shot.GetComponent<SpriteRenderer>().enabled = true;
                Shot.velocity = new Vector2((Cam.ScreenToWorldPoint(Input.mousePosition).x - this.transform.position.x), (Cam.ScreenToWorldPoint(Input.mousePosition).y - this.transform.position.y)).normalized * ShotSpeed;
            }
        }
    }
    public void BroadcastToEnemy(string ToBroadcast)
    {
        foreach(GameObject g in Main.publicvariables.AllEnemy)
        {
            g.SendMessage(ToBroadcast);
        }
    }
    public void TakeDmg(int dmg)
    {
        Health -= dmg;
    }
    private float LinearDistance(Vector2 position1, Vector2 position2)
    {
        return (float)(Math.Sqrt(Math.Pow((position1.x - position2.x), 2) + Math.Pow(position1.y - position2.y, 2)));
    }

    private float LinearAngle(Vector2 position1, Vector2 position2)
    {
        float pos1x = position1.x;
        float pos1y = position1.y;
        float pos2x = position2.x;
        float pos2y = position2.y;
        float ans = 0;
        if (pos2x > pos1x && pos2y > pos1y)
        {
            ans = Mathf.Atan((pos2y - pos1y) / (pos2x - pos1x)) * 180 / Mathf.PI;
            //ans += 180;
        }
        else
        {
            ans = Mathf.Atan((pos1y - pos2y) / (pos1x - pos2x)) * 180 / Mathf.PI;
        }
        if (ans < 0)
        {
            ans += 360;
        }

        return ans;
    }
}
