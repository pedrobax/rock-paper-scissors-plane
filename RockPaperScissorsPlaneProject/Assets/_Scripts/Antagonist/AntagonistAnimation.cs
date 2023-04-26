using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistAnimation : MonoBehaviour
{
    Animator animator;
    public Vector3 currentVelocity;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(GetTransformVelocity());
        Debug.Log(currentVelocity);
        SetAnimatorSpeeds();
    }

    void SetAnimatorSpeeds()
    {
        animator.SetFloat("ForwardVelocity", currentVelocity.z * 10);
        animator.SetFloat("SidewaysVelocity", currentVelocity.x * 10);
    }

    IEnumerator GetTransformVelocity()
    {
        Vector3 startingPosition = transform.parent.position;
        yield return new WaitForFixedUpdate();
        Vector3 endingPosition = transform.parent.position;
        Vector3 delta = endingPosition - startingPosition;
        currentVelocity = delta;
    }
}
