using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    //player position
    Vector3 playerPos;

    //collided object with the player
    GameObject collObject;

    //is the action to kill activated?
    bool killActive;

    //kill target of player
    public GameObject target;

    private NavMeshAgent agent;

    //practically a pointer to the KillButton-Script
    private KillButtonController killButtonController;

    // Start is called before the first frame update
    void Start()
    {
        //player position, first level
        playerPos = new Vector3(-20, 0.5f, -40);
        this.transform.position = playerPos;

        target = this.gameObject;
        killActive = false;
        agent = this.GetComponent<NavMeshAgent>();

        killButtonController = GameObject.Find("KillButton").GetComponent<KillButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(ray.origin, hit.point);

        //this makes sure, the player is following a moving target until another raycast has been made or the target is killed
        if(target != this.gameObject)
        {
            agent.SetDestination(target.transform.position);
        }

        //movement control through mouse raycasting and navMeshAgent
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            /* if the Raycast hits a target and the mouse does not hover over an UI element
             * note to "EventSystem.current.IsPointerOverGameObject()": method returns true if mouse hovers over an UI element
             *      method is currently needed because Raycast ignores UI elements
             */
            if (Physics.Raycast(ray, out hit) && !EventSystem.current.IsPointerOverGameObject())
            {
                agent.SetDestination(hit.point);

                if (killActive && hit.collider.tag == "Enemy")
                {
                    target = hit.rigidbody.gameObject;
                    Debug.Log("target aquired: " + target);
                } else
                {
                    target = this.gameObject;
                }
            }

            
        }

        //if key is pressed, keyActions() is executed
        if(Input.inputString != "")
        {
            keyActions();
        }
    }


    /* OnTriggerEnter:
     * collObject will be set to the collided object with the player
     */
    private void OnTriggerEnter(Collider other)
    {
        collObject = other.gameObject;

        if(collObject == target && killActive)
        {
            //destroy target and target reset
            target = this.gameObject;
            Destroy(collObject);
        }
    }

    /* OnTriggerStay:
     */
    private void OnTriggerStay(Collider other)
    {
        collObject = other.gameObject;

        if (collObject == target && killActive)
        {
            target = this.gameObject;
            Destroy(collObject);
        }
    }

    /* OnTriggerExit:
     * collObject will be reset to player gameObject
     */
    private void OnTriggerExit(Collider other)
    {
        //is necessary in case another collider has already been entered
        if (collObject == other)
        {
            collObject = this.gameObject;
        }
    }
    

    /* keyActions:
     * collects all possible key actions by the player ...
     * ... and refers to fit methods
     * does not include mouse actions
     */
    private void keyActions()
    {
        string keyInput = Input.inputString;

        switch (keyInput)
        {
            case "a":
                KillActive();
                break;

            default:
                break;
        }
    }

    /* killActive()
     * method that changes killActive and sets the text on the KillButton
     */
    public void KillActive()
    {
        killActive = !killActive;
        killButtonController.SetText();

        //covers case in which the kill target was selected but the kill deactivated
        //makes player stop immediately
        if(!killActive && target != this.gameObject)
        {
            target = this.gameObject;
            agent.destination = this.transform.position;
        }
    }
}
