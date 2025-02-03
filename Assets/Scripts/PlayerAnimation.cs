using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private float directionDampTime = 0.25f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", playerMovement.GetCurrentSpeed() / playerMovement.runningSpeed);
        animator.SetFloat("Direction", playerMovement.x directionDampTime);
    }
}
