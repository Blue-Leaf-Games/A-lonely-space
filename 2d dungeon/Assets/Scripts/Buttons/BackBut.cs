using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class BackBut : MonoBehaviour
    {
        public void LoadScene()
        {
            
            SceneManager.LoadScene(publicvariables.lastScene[publicvariables.listlength(publicvariables.lastScene) - 1]);
        }
    }
}
