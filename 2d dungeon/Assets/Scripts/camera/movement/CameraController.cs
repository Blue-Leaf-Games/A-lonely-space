using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movespeed;
    public Transform movepoint;
    public float dist;
    public float constz;

    // Start is called before the first frame update
    void Start()
    {
        movepoint.parent = null;
        constz = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, constz), new Vector3(movepoint.position.x, movepoint.position.y, constz), movespeed * Time.deltaTime);
    }
}
