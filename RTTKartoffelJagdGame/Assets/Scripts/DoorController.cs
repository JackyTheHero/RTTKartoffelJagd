using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject doorLeft;
    private GameObject doorRight;

    private Vector3 doorLeftPiv;
    private Vector3 doorRightPiv;

    private bool doorActivated;


    // Start is called before the first frame update
    void Start()
    {
        doorLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        doorLeft.transform.parent = this.transform;
        doorLeft.transform.localScale = new Vector3(8,6,2);
        doorLeft.transform.position = this.transform.position + new Vector3(-4, 0, 0);
        doorLeftPiv = this.transform.position + new Vector3(-8, 0, 0);
        doorLeft.GetComponent<Renderer>().material = GameObject.Find("box1").GetComponent<Renderer>().material;

        //small rotation because negative rotation does not work directly; using Quaternion.Inverse instead
        doorLeft.transform.Rotate(0,0.01f,0);
        doorLeft.transform.rotation = Quaternion.Inverse(doorLeft.transform.rotation);

        doorRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        doorRight.transform.parent = this.transform;
        doorRight.transform.localScale = doorLeft.transform.localScale;
        doorRight.transform.position = this.transform.position + new Vector3(4, 0, 0);
        doorRightPiv = this.transform.position + new Vector3(8, 0, 0);
        doorRight.GetComponent<Renderer>().material = GameObject.Find("box1").GetComponent<Renderer>().material;

        doorActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //remember: doorLeft rotates the other way; negative rotation only works for numbers above zero
        if (doorActivated && doorLeft.transform.rotation.eulerAngles.y > 270f)
        {
            Debug.Log("doors are moving: " + doorLeft.transform.rotation.eulerAngles.y);
            doorLeft.transform.RotateAround(doorLeftPiv, Vector3.up, -20f * Time.deltaTime);
            
            Debug.Log("doorLeft now: " + doorLeft.transform.rotation.eulerAngles.y);
            doorRight.transform.RotateAround(doorRightPiv, Vector3.up, 20f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            doorActivated = true;
        }
    }
}
