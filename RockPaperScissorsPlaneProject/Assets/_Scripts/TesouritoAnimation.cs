using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesouritoAnimation : MonoBehaviour
{
    public AccelerateTowardsPlayerAction accelerate;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(accelerate == null)
        {
            transform.parent.transform.parent.GetComponent<AccelerateTowardsPlayerAction>();
        }
            
    }

    void Update()
    {
        if(accelerate.isActing)
        {
            //Debug.Log("tesourito is moving");
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }	
    }
}
