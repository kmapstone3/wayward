using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CharacterType
{
    Woodsman,
    Owl,
    Both
}

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D mainCollider;

    public CameraController cameraController;

    public Character other;

    public float moveSpeed;
    public float jumpSpeed;

    public float deathAnimationDuration;

    protected bool isMoving = false;
    protected bool isGrounded;
    protected bool isDead = false;

    protected Transform followTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateAnimationParameters();

        if(!IsCharacterActive() && followTransform != null)
            Follow();
    }

    protected virtual void UpdateAnimationParameters()
    {
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Grounded", isGrounded);
    }

    public virtual void TestInput()
    {
        //defaults
        isMoving = false;

        // Jump
        if(Input.GetKeyDown(KeyCode.Space) && CanJump())
            Jump();

        // Swap Characters
        if(Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(Swap());

        // Force-Death
        if(Input.GetKeyDown(KeyCode.R))
            StartCoroutine(DieCo());
    }

    public virtual IEnumerator Swap()
    {
        // When swapping, set other character to be the active character and the camera target; also disable other character's follow
        cameraController.SetActiveCharacter(other);
        cameraController.SetTarget(other.transform);
        other.SetFollow(null);

        yield return null;

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

    void Follow()
    {
        Vector2 diff = followTransform.position - transform.position;
        if(diff.magnitude < 0.2f)
            return;

        Vector3 dir = diff.normalized;

        Move(dir);
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

    public void Die()
    {
        StartCoroutine(DieCo());
    }

    public IEnumerator DieCo()
    {
        if(isDead)
            yield break;

        isDead = true;

        // Disable collider and gravity
        mainCollider.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Make sure camera is focused on this character
        cameraController.SetActiveCharacter(this);
        cameraController.SetTarget(transform);

        anim.SetTrigger("Die");

        yield return new WaitForSeconds(deathAnimationDuration);

        // Reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetFollow(Transform target)
    {
        followTransform = target;
    }

    protected virtual bool CanJump() => isGrounded;

    public bool IsCharacterActive() => cameraController.GetActiveCharacter() == this;

    public bool IsDead() => isDead;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Ground Check
        if(!other.isTrigger)
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Ground Check
        if(!other.isTrigger)
            isGrounded = false;
    }
}
