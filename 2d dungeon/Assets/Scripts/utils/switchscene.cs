using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchscene : MonoBehaviour
{
    public static Scene sc;
    public void switchsc()
    {
        SceneManager.SetActiveScene(sc);
    }

    public void setsc(Scene scene)
    {
        sc = scene;
    }

}
