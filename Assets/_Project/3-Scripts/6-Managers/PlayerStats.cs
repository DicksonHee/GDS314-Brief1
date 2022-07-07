using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int maxHealth;
    private static int currentHealth;

    private static event Action OnPlayerDeath;
    private static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
}