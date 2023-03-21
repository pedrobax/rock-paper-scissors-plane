using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LevelArea levelArea;
    [SerializeField] Rigidbody rb;
    public PlayerHealth playerHealth;

    [SerializeField] float rockMaxSpeed;
    [SerializeField] float paperMaxSpeed;
    [SerializeField] float scissorsMaxSpeed;
    [SerializeField] float currentMaxSpeed;

    Vector3 moveVelocity;
    float maxHorizontalPosition;
    float minHorizontalPosition;
    float maxVerticalPosition;
    float minVerticalPosition;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        SetMaxPositions();
    }


    void Update()
    {
        SetSpeedBasedOnCurrentType();
        SetMoveVelocity();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        MovePlayerBackToArea();
    }

    void SetMaxPositions()
    {
        maxHorizontalPosition = levelArea.GetHorizontalAreaLimit();
        minHorizontalPosition = levelArea.GetHorizontalAreaLimit() * -1;
        maxVerticalPosition = levelArea.GetVerticalAreaLimit();
        minVerticalPosition = levelArea.GetVerticalAreaLimit() * -1;
    }

    void SetMoveVelocity()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * currentMaxSpeed;
    }

    void MovePlayer()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void SetSpeedBasedOnCurrentType()
    {
        if (playerHealth.currentType == PlayerHealth.PlayerType.Rock) currentMaxSpeed = rockMaxSpeed;
        if (playerHealth.currentType == PlayerHealth.PlayerType.Paper) currentMaxSpeed = paperMaxSpeed;
        if (playerHealth.currentType == PlayerHealth.PlayerType.Scissors) currentMaxSpeed = scissorsMaxSpeed;
    }

    void MovePlayerBackToArea()
    {
        Vector3 maxHorizontalLimit = new(maxHorizontalPosition, rb.position.y, rb.position.z);
        Vector3 minHorizontalLimit = new(minHorizontalPosition, rb.position.y, rb.position.z);
        if (rb.position.x > maxHorizontalPosition) rb.MovePosition(maxHorizontalLimit);
        if (rb.position.x < minHorizontalPosition) rb.MovePosition(minHorizontalLimit);


        Vector3 maxVerticalLimit = new(rb.position.x, rb.position.y, maxVerticalPosition);
        Vector3 minVerticalLimit = new(rb.position.x, rb.position.y, minVerticalPosition);
        if (rb.position.z > maxVerticalPosition) rb.MovePosition(maxVerticalLimit);
        if (rb.position.z < minVerticalPosition) rb.MovePosition(minVerticalLimit);
    }
}
