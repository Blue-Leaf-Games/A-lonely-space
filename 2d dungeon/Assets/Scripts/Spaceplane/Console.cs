using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public Rigidbody2D player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("1");
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject == this)
            {
                Debug.Log("2");
                if (Vector2.Distance(this.transform.position,player.transform.position) == 0.16f)
                {
                    Debug.Log("works");
                }
            }
        }
    }
}
