using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerMovementActions
{
    public static void MovePlayerToLocation(Vector3 position, GameObject playerObject)
    {
        playerObject.transform.position = position;
    }
}
