using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    Button[] allButtons;
    Button killButton;

    // Start is called before the first frame update
    void Start()
    {
        allButtons = GameObject.FindObjectsOfType<Button>();
        SetAllButtons();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /* SetAllButtons():
     * runs through the allButtons array and sets the script Button objects to scene Button objects
     */
    void SetAllButtons()
    {
        for(int i = 0; i < allButtons.Length - 1; i++)
        {
            if(allButtons[i].name == "killButton")
            {
                killButton = allButtons[i];
            }
        }
    }
}
