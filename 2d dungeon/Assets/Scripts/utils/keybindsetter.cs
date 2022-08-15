using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

public class keybindsetter : MonoBehaviour
{
    public KeyCode keypressed;
    public void setkeybind(Button button, Text txt)
    {
        txt.text = txt.text.Split(' ')[0] + " "+ keypressed.ToString();
        jsonconverter.setjson(Application.dataPath+"/Scripts/jsons/keybinds.json", button.name.ToString(), keypressed.ToString());
    }

    void Update()
    {

    

    }
}
