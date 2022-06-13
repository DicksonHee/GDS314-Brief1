using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAssignment : MonoBehaviour
{
    public Transform[] chairPositions;
    public List<bool> spawnList;
    private int numberOfChairs;
    public GameObject chair;



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
            AddBool();
            ArrayShuffle();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetList();
        }

    }


    void ArrayShuffle()
    {
        // number of chairs to be thrown
        

        // which lanes are selected

        
        for (int k = 0; k < spawnList.Count; k++)
        {
            bool temp = spawnList[k];
            int randomiser = Random.Range(k, spawnList.Count);
            spawnList[k] = spawnList[randomiser];
            spawnList[randomiser] = temp;
        }

    }

    void AddBool()
    {
        numberOfChairs = Random.Range(1, chairPositions.Length);
        // for how many chair positions there are
        for (int ii = 0; ii < chairPositions.Length; ii++)
        {
            // if the number of chairs determined before is more than 0 then add a true value
            if (0 < numberOfChairs)
            {
                spawnList.Add(true);
                numberOfChairs--;
            }
            // when you run out of chairs you then must add a false value until the array is full
            else
            {
                spawnList.Add(false);
            }
                
        }
    }
    void ResetList()
    {
        spawnList.Clear();
    }
    
        

        
    

}
