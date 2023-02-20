using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Grounded,
    Flying
}

public class Owl : Character
{
    private MovementState movementState = MovementState.Grounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        // If Owl is flying, no gravity
        if(movementState == MovementState.Flying)
            rb.gravityScale = 0;
        else
            rb.gravityScale = 1;
    }

    public override void TestInput()
    {
        base.TestInput();

        // Only move vertically if flying; always move horizontally
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), 0);
        if(movementState == MovementState.Flying)
            dir += new Vector2(0, Input.GetAxis("Vertical"));

        Move(dir);

        // If you press Space while airborne, stop flying
        if(Input.GetKeyDown(KeyCode.Space) && movementState == MovementState.Flying)
            SetMovementState(MovementState.Grounded);

        // If you press W while airborne, start flying
        if(Input.GetKeyDown(KeyCode.W) && !isGrounded)
            SetMovementState(MovementState.Flying);
    }

    // OWL OVERRIDES CANJUMP() SO THAT JUMPING IS DISABLED WHEN FLYING
    protected override bool CanJump()
    {
        return base.CanJump() && movementState == MovementState.Grounded;
    }

    public void SetMovementState(MovementState movementState)
    {
        this.movementState = movementState;

        // Reset velocity when switching to flying state
        if(movementState == MovementState.Flying)
            rb.velocity = Vector2.zero;
    }
}
