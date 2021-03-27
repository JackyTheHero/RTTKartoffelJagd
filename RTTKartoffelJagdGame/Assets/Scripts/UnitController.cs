using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;

public class UnitController : MonoBehaviour
{
    private NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        switch (EnemyReferent())
        {
            case "1":
                StartCoroutine(EnemyOneRoutine());
                break;

            case "2":
                StartCoroutine(EnemyTwoRoutine());
                break;

            case "3":
                StartCoroutine(EnemyThreeRoutine());
                break;

            case "4":
                StartCoroutine(EnemyFourRoutine());
                break;

            case "5":
                this.transform.Rotate(0, 180, 0);
                break;

            case "6":
                this.transform.Rotate(0, 180, 0);
                break;

            default:
                Debug.Log("enemy number " + EnemyReferent() + " does not exist!");
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        IsPlayerSeen();
        
        Debug.DrawRay(this.transform.position, transform.forward, Color.red);
        Debug.DrawLine(ray.origin, hit.point);

        /*
        if(IsPlayerSeen())
        {
            Debug.Log(this.name + " says: VERLOREN");
        }
        */

        if (this.name == "enemy5" && GameObject.Find("enemy4"))
        {
            this.transform.LookAt(GameObject.Find("enemy4").transform);
        }

        if(this.name == "enemy4" && this.transform.position.z < 28f && GameObject.Find("enemy5"))
        {
            this.transform.LookAt(GameObject.Find("enemy5").transform);
        }

    }

    private IEnumerator EnemyOneRoutine()
    {
        yield return DestWaitHelpRoutine(new Vector3(-33, 0, -14), 2f);
        yield return DestWaitHelpRoutine(new Vector3(-33, 0, 5), 2f);
        yield return DestWaitHelpRoutine(new Vector3(-45, 0, 5), 2f);
        yield return DestWaitHelpRoutine(new Vector3(-45, 0, -14), 2f);

        StartCoroutine(EnemyOneRoutine());
    }

    private IEnumerator EnemyTwoRoutine()
    {
        yield return DestHelpRoutine(new Vector3(-17, 0, -14));
        yield return DestWaitHelpRoutine(new Vector3(20, 0, -23), 5f);
        yield return DestHelpRoutine(new Vector3(27, 0, 16));
        yield return DestHelpRoutine(new Vector3(-21, 0, 35));
        yield return DestHelpRoutine(new Vector3(-26, 0, -9));

        StartCoroutine(EnemyTwoRoutine());
    }

    private IEnumerator EnemyThreeRoutine()
    {
        yield return DestWaitHelpRoutine(new Vector3(-1, 0, 19), 5f);
        yield return DestWaitHelpRoutine(new Vector3(8, 0, 7), 5f);
        StartCoroutine(EnemyThreeRoutine());
    }

    private IEnumerator EnemyFourRoutine()
    {
        yield return DestWaitHelpRoutine(new Vector3(27, 0, 26), 10f);
        yield return DestWaitHelpRoutine(new Vector3(32, 0, 34), 3f);
        yield return DestWaitHelpRoutine(new Vector3(25, 0, 47), 5f);
        StartCoroutine(EnemyFourRoutine());
    }


    /* DestHelpRoutine(Vector3 dest):
    * takes over repeating steps from the enemy routines
    * include in coroutine if you want to set a destination:
    * yield return DestHelpRoutine(new Vector3(xDest, 0, zDest));
    */
    private IEnumerator DestHelpRoutine(Vector3 dest)
    {
        agent.destination = dest;
        yield return new WaitUntil(() => checkDestinationDistance());
    }

    /* DestWaitHelpRoutine(Vector3 dest, float timeToWait):
    * acts like DestHelpRoutine() but...
    * ...includes waiting time after destination is reached:
    * yield return DestHelpRoutine(new Vector3(xDest, 0, zDest), timeToWait);
    */
    private IEnumerator DestWaitHelpRoutine(Vector3 dest, float timeToWait)
    {
        agent.destination = dest;
        yield return new WaitUntil(() => checkDestinationDistance());
        yield return new WaitForSeconds(timeToWait);
    }

        private string EnemyReferent()
    {
        if (this.name.Contains("enemy"))
        {
            return this.name.Substring(5);
            //return int.Parse(this.name.Substring(5));
        }

        return "-1";
    }

    //help method to check if the distance the NavMeshAgent has to travel is almost zero
    private bool checkDestinationDistance()
    {
        return (agent.destination - this.transform.position).magnitude < new Vector3(0.1f, 0.5f, 0.1f).magnitude;
    }

    /* IsPlayerSeen():
     * checks through a simple forward Raycast if the player is near
     */
    private bool IsPlayerSeen()
    {
        ray = new Ray(this.transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 20f))
        {
            if (hit.collider.name == "player")
            {
                SceneManager.LoadScene("PostGameScene");
                Debug.Log(this.name + " says: VERLOREN");
                return true;
            }
        }
        return false;
    }
}
