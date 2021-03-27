using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCreator: MonoBehaviour
{
    public List<GameObject> unitList;
    private GameObject unitPrefab;
    public List<Vector3> unitPositions;
    private GameObject unitParent;

    // Start is called before the first frame update
    void Start()
    {
        //unitList allows access to created units
        unitList = new List<GameObject>();

        unitPrefab = GameObject.Find("enemyPrefab");

        //Empty that cleans up the hierarchy a bit
        //parents all units
        unitParent = new GameObject();
        unitParent.name = "enemies";

        //stores position of every unit for easy access
        unitPositions = new List<Vector3>()
        {
            new Vector3(-33,0,-14),
            new Vector3(-17,0,-14),
            new Vector3(4,0,12),
            new Vector3(25,0,27),
            new Vector3(34,0,28),
            new Vector3(25,0,-31),
        };

        //units are created, based on how many positions were given
        createAllUnits(unitPositions.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* createUnit:
     * param: pos --> position on which the new unit will be created
     * method that creates on instance of unitPrefab
     */
    public void createUnit(Vector3 pos)
    {
        //unit does ONLY exist within createUnit()
        //to access the unit afterwards, use unitList
        GameObject unit = Instantiate(unitPrefab);

        unit.transform.parent = unitParent.transform;

        //unit gets its position assigned
        unit.GetComponent<NavMeshAgent>().enabled = false;
        unit.transform.position = pos;

        unitList.Add(unit);
        unit.name = "enemy" + unitList.Count;
        unit.GetComponent<NavMeshAgent>().enabled = true;
    }

    /* createAllUnits:
     * param: unitCount --> determines how many units will be created
     * creates all units by repeatedly calling createUnit
     * seperated from the start function so that a reset of the units is easier to execute
     */
    public void createAllUnits(int unitCount)
    {
        for (int i = 0; i < unitCount; i++)
        {
            createUnit(unitPositions[unitList.Count]);
        }
    }
}