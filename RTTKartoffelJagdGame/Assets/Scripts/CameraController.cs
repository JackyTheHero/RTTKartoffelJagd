using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //main Camera that always follows freeLooker
    GameObject mainCamera;


    //Camera that is responsible for the minimap; here used for minimap rotation
    //minimapCamera not currently used
    //GameObject minimapCamera;

    //Empty that follows the player unless freelook is activated
    //does NOT follow rotation of player
    GameObject freeLooker;

    //Player Figure
    GameObject player;

    //boolean that checks if camera is fixed on player
    public bool playerFix;

    //help variables for the freelook camera
    private float mouseXStart;
    private float mouseXNow;
    private float mouseYStart;
    private float mouseYNow;
    private Quaternion freeLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("mainCamera");
        //minimapCamera = GameObject.Find("minimapCamera");
        freeLooker = GameObject.Find("freeLooker");
        player = GameObject.Find("player");

        //freeLooker follows player
        resetCamera();
        playerFix = true;

        mouseXStart = Input.mousePosition.x;
        mouseYStart = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //freeLooker position is synchronized with player position
        //original idea of transform.parent would not work because rotation should not change
        if (playerFix)
        {
            freeLooker.transform.position = player.transform.position;
        }

        if (Input.inputString != "")
        {
            CameraActions();
        }
    }

    private void CameraActions()
    {
        //camera returns to player
        if (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1))
        {
            resetCamera();
        }

        //camera rotates 45 degress to the right
        if (Input.GetKeyDown("q"))
        {
            freeLookRotation *= Quaternion.Euler(0, 45f, 0);
            freeLooker.transform.rotation = freeLookRotation;
            //minimapCamera.transform.RotateAround(this.transform.position, Vector3.up, 45f);
        }

        //camera rotates 45 degress to the left
        if (Input.GetKeyDown("e"))
        {
            freeLookRotation *= Quaternion.Euler(0, -45f, 0);
            freeLooker.transform.rotation = freeLookRotation;
            //minimapCamera.transform.RotateAround(this.transform.position, Vector3.up, -45f);
        }


        //TODO: MouseButton 2 does not seem to work in build
        if (Input.GetKey("r") || Input.GetMouseButton(2))
        {
            mouseXNow = Input.mousePosition.x;
            mouseYNow = Input.mousePosition.y;

            freeLooker.transform.position += freeLookRotation * new Vector3((mouseXNow - mouseXStart) / 100, 0, (mouseYNow - mouseYStart) / 100);
        }

        //TODO: MouseButton 2 does not seem to work in build
        if (Input.GetKeyDown("r") || Input.GetMouseButtonDown(2))
        {
            playerFix = false;
            freeLooker.transform.SetParent(null);

            //set this later to Screen.width and .height; camera jumps to mousePosition now
            mouseXStart = Input.mousePosition.x;
            mouseYStart = Input.mousePosition.y;
            mouseXNow = mouseXStart;
            mouseYNow = mouseYStart;
        }

    }

    /* resetCamera:
     * resets freeLooker and therefore the camera to the current player position
     */
    private void resetCamera()
    {
        playerFix = true;
        freeLooker.transform.position = player.transform.position;
        //freeLooker.transform.rotation = player.transform.rotation;
        freeLookRotation = Quaternion.Euler(0,0,0);
    }
}
