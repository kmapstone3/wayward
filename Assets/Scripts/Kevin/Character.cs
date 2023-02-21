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
        UpdateAnimationParameters();
    }

    protected virtual void UpdateAnimationParameters()
    {
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Grounded", isGrounded);
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

        isMoving = false;
    }

    public virtual void Move(Vector2 dir)
    {
        if(dir == Vector2.zero)
            return;

        isMoving = true;

        // If facing in direction opposite of motion, flip transform
        if(transform.localScale.x > 0 != dir.x > 0)
            FlipTransform();

        transform.position += (Vector3) dir.normalized * moveSpeed * Time.deltaTime;
    }

    void FlipTransform()
    {
        // Negate scale x, keep scale y
        transform.localScale *= new Vector2(-1, 1);
    }

    public virtual void Jump()
    {
        // ANIM JUMP
        anim.SetTrigger("Jump");
        isGrounded = false;

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
