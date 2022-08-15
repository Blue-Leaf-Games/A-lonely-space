using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dockingportcollider : MonoBehaviour
{
    public Collider2D port;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(publicvariables.dockingportopen )
        {
            port.enabled = true;
        }
        else
        {
            port.enabled = false;
        }
    }
}
