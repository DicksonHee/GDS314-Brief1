using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChairAssignment : MonoBehaviour
{
    public List<LaneGroup> laneGroups;
    public GameObject chair;
    public ChairScraper scraper;

	private void Start()
	{
        InvokeRepeating(nameof(TurnStart), 1f, 5f);
	}

	private void TurnStart()
    {
        ShowLanes();
        Invoke(nameof(ConfirmLanes), 2f);
    }

    private void ConfirmLanes()
    {
        ConfirmSpawn(scraper.CalculateHighest(2));
    }

	private void ShowLanes()
    {
        for (int ii = 0; ii < laneGroups.Count; ii++)
        {
            laneGroups[ii].Spawn(chair);
        }
    }

    private void ConfirmSpawn(List<LaneDirections> targetDir)
    {
        foreach (LaneGroup group in laneGroups)
        {
            foreach (LaneDirections dir in targetDir)
            {
                if (group.direction == dir)
                {
                    group.ConfirmSpawn();
                    break;
                }
            }
        }
	}
}

[Serializable]
public class LaneGroup
{
    public LaneDirections direction;
    public List<Transform> spawnPositions;

    [HideInInspector] public List<bool> spawnList = new();
    private List<GameObject> _spawnedObjects = new();

    // Spawn chairs at location
    public void Spawn(GameObject spawnObject)
    {
        AddChairs();
        _spawnedObjects.Clear();
        for (int ii = 0; ii < spawnPositions.Count; ii++)
        {
            if (spawnList[ii] == true)
            {
                _spawnedObjects.Add(UnityEngine.Object.Instantiate(spawnObject, spawnPositions[ii]));
            }
        }
    }

    public void ConfirmSpawn()
    {
        foreach (GameObject gameObject in _spawnedObjects)
        {
            gameObject.GetComponent<ChairObject>().SpawnConfirmed(direction);
        }
    }

    // Add numberOfChairs to spawn to spawnList
    private void AddChairs()
    {
        int numberOfChairs = spawnPositions.Count - 1;// Random.Range(1, group.spawnPositions.Count - 1);

        spawnList.Clear();
        for (int ii = 0; ii < spawnPositions.Count; ii++)
        {
            if (0 < numberOfChairs)
            {
                spawnList.Add(true);
                numberOfChairs--;
            }
            else
            {
                spawnList.Add(false);
            }
        }

        ShuffleArray();
    }

    // Randomise spawnList array
    private void ShuffleArray()
    {
        for (int ii = 0; ii < spawnList.Count; ii++)
        {
            bool temp = spawnList[ii];
            int randomiser = Random.Range(ii, spawnList.Count);
            spawnList[ii] = spawnList[randomiser];
            spawnList[randomiser] = temp;
        }
    }
}

public enum LaneDirections
{
    N,
    E,
    S,
    W,
    None
}
