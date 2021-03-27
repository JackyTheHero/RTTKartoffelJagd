using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkButtonController : MonoBehaviour
{
    public void OpenLink()
    {
        Application.OpenURL("https://github.com/JackyTheHero");
    }
}
