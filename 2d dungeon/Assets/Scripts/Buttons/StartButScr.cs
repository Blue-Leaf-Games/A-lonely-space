using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Main
{
    public class StartButScr : MonoBehaviour
    {
        public void LoadScene()
        {
            publicvariables.lastScene.Add(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("spaceplane");
        }
    }
}
