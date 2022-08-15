using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed;
    public Transform movepoint;
    public float dist;
    public float constz;
    public LayerMask colliders;
    // Start is called before the first frame update
    void Start()
    {
        movepoint.parent = null;
        constz = transform.position.z;
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
                if (!Physics2D.OverlapBox(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal")*dist,0f,0f), new Vector2(dist,dist),0))
                {
                    Main.publicvariables.playermoving = true;
                    movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * dist, 0f, 0f);
                    Main.publicvariables.playerdirection = 2*Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapBox(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * dist,  0f), new Vector2(dist, dist), 0))
                {
                    Main.publicvariables.playermoving = true;
                    movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * dist, 0f);
                    Main.publicvariables.playerdirection = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
                }
            }
        }
    }
}
