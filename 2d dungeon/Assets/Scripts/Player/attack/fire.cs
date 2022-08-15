using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Main
{
    public class fire : MonoBehaviour
    {
        public Rigidbody2D projectile;
        public bool shooting = false;
        public float velocity;
        public KeyCode shoot;
        public Rigidbody2D player;
        public Rigidbody2D weapon;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            projectile.GetComponent<PolygonCollider2D>().enabled = false;
            projectile.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics2D.OverlapBox(projectile.position, new Vector2(0.16f, 0.02f),0f)&&shooting)
            {
                projectile.GetComponent<PolygonCollider2D>().enabled = false;
                projectile.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (!shooting && Input.GetKey(shoot))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                Vector2 direction = mousePosition - player.transform.position;
                float angle = Vector2.SignedAngle(Vector2.right, direction);
               
                projectile.GetComponent<PolygonCollider2D>().enabled = true;
                projectile.GetComponent<SpriteRenderer>().enabled = true;
                projectile.position = player.position;
                projectile.transform.eulerAngles = new Vector3(0, 0, angle);
                projectile.velocity = new Vector2(velocity,0);
                shooting = true;
                publicvariables.playerattacking = true;
            }

        }
    }
}
