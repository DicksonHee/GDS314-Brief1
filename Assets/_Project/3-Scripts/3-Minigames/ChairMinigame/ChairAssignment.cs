using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAssignment : MonoBehaviour
{
    public Transform[] chairPositions;
    public GameObject chair;

    private List<bool> spawnList;

    // USE THE LIST TO RANDOMISE THE CHAIRS USED
    // ALSO ADD THE CHAIRS TO SPAWN NUMBER OF TRUE VARIABLES TO THE LIST
    // ADD FALSE AS MANY TIMES UNTIL THE SPAWN LISTS COUNT EQUALS THE NUMBER OF LANES
    // CHECK WHERE THE SPAWN LIST IS TRUE AND THEN SPAWN A CHAIR AT THE CORRESPONDING POSITION
    // MAY NEED CHAIR NUMBER FROM THE VARIABLE numberOfChairs

    private void Start()
    {
        spawnList = new List<bool>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))

        {
            AddChairs(Random.Range(1, chairPositions.Length - 1));
            ArrayShuffle();
            SpawnChairs();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetList();
        }

    }


    // Randomise spawnList array
    private void ArrayShuffle()
    {
        for (int ii = 0; ii < spawnList.Count; ii++)
        {
            bool temp = spawnList[ii];
            int randomiser = Random.Range(ii, spawnList.Count);
            spawnList[ii] = spawnList[randomiser];
            spawnList[randomiser] = temp;
        }

    }

    // Spawn chairs at location
    private void SpawnChairs()
    {
        for (int ii = 0; ii < chairPositions.Length; ii++)
        {
            if (spawnList[ii] == true)
            {
                Instantiate(chair, chairPositions[ii]);
                
            }
        }
    }
    
    // Add numberOfChairs to spawn to spawnList
    private void AddChairs(int numberOfChairs)
    {
        ResetList();
        // for how many chair positions there are
        for (int ii = 0; ii < chairPositions.Length; ii++)
        {
            if (0 < numberOfChairs) // if the number of chairs determined before is more than 0 then add a true value
            {
                spawnList.Add(true);
                numberOfChairs--;
            }
            else // when you run out of chairs you then must add a false value until the array is full
            {
                spawnList.Add(false);
            }
                
        }
    }

    // Reset spawnList
    private void ResetList()
    {
        spawnList.Clear();
    }
}
