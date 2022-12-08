using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Main
{
    
    public class Console : MonoBehaviour
    {
        public Rigidbody2D ConsoleSprite;

        public void Update()
        {
            if (Input.GetKey(KeyCode.F) && Vector2.Distance(this.transform.position, ConsoleSprite.position)<=0.32)
            {
                publicvariables.lastScene.Add(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Map1");
            }
        }
    }
}
