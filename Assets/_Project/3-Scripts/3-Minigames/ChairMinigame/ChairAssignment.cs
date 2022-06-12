using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAssignment : MonoBehaviour
{
    public Transform[] chairPositions;
    private List<bool> spawnList;
    private int selectedPosition;
    private int numberOfChairs;
    private Transform currentPosition;
    public GameObject chair;
    private int j;
    

    // USE THE LIST TO RANDOMISE THE CHAIRS USED
    // ALSO ADD THE CHAIRS TO SPAWN NUMBER OF TRUE VARIABLES TO THE LIST
    // ADD FALSE AS MANY TIMES UNTIL THE SPAWN LISTS COUNT EQUALS THE NUMBER OF LANES
    // CHECK WHERE THE SPAWN LIST IS TRUE AND THEN SPAWN A CHAIR AT THE CORRESPONDING POSITION
    // MAY NEED CHAIR NUMBER FROM THE VARIABLE numberOfChairs

    void ArraySelect()
    {
        // number of chairs to be thrown
        numberOfChairs = Random.Range(1, chairPositions.Length - 1);
        // which lanes are selected


        //for (int i = 0; spawnList.Count < numberOfChairs; i++)
        //{
            // position of the chair that will spawn = a point in the chair positions
            //selectedPosition = Random.Range(0, chairPositions.Length);
            // if any of the previous positions were already used then do another (may be a little process heavy)

            


            //for (j = 0; j < previousPositions.Length; j++)
            //{
            //    if (previousPositions[j] != selectedPosition)
            //    {
            //        Instantiate(chair, chairPositions[selectedPosition]);
            //    }
            //}
            

                
            // 
    }
        

        
    

}
