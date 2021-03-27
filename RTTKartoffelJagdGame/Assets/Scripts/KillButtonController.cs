using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillButtonController : MonoBehaviour
{
    private Text buttonText;
    private bool activated = false;

    public void SetText()
    {
        activated = !activated;
        buttonText = transform.Find("Text").GetComponent<Text>();

        if (activated)
        {
            buttonText.text = "Kill activated";
        } else
        {
            buttonText.text = "Kill deactivated";
        }
        
    }
}
