using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlaneController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "player")
        {
            SceneManager.LoadScene("PostGameScene");
            Debug.Log("Geschafft!");
        }
    }
}
