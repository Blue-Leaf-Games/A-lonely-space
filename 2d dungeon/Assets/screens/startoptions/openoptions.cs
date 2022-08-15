using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openoptions : MonoBehaviour
{
    public string options;
 void optionscreen()
    {
        SceneManager.LoadScene(options); 
    }
}
