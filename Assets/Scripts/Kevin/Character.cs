using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public CameraController cameraController;

    public Character other;

    public float moveSpeed;
    public float jumpSpeed;

    [SerializeField] protected bool isMoving = false;
    protected bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        anim.SetBool("Moving", isMoving);
    }

    public virtual void TestInput()
    {
        isMoving = false;

        if(Input.GetKeyDown(KeyCode.Space) && CanJump())
            Jump();

        if(Input.GetKeyDown(KeyCode.Q))
            Swap();
    }

    public virtual void Swap()
    {
        cameraController.SetActiveCharacter(other);
        cameraController.SetTarget(other.transform);
    }

    public virtual void Move(Vector2 dir)
    {
        isMoving = true;

        transform.position += (Vector3) dir.normalized * moveSpeed * Time.deltaTime;
    }

    public virtual void Jump()
    {
        // ANIM JUMP
        anim.SetTrigger("Jump");

        rb.velocity = Vector2.up * jumpSpeed;
    }

    protected virtual bool CanJump()
    {
        return isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ground Check
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Ground Check
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
    }
}
