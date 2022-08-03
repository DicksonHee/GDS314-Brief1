using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Animator playerAnim;
    private static readonly int Death = Animator.StringToHash("DeathActive");

    public void UponDeath()
    {
        Debug.Log("Death Accomplished");
        playerAnim.SetTrigger(Death);
        
    }

}
