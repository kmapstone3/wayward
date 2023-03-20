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

public enum MovementState
{
    Grounded,
    Flying
}

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D mainCollider;

    public AudioSource jumpAudio;

    public CameraController cameraController;

    public Character other;

    public float moveSpeed;
    public float jumpSpeed;

    public float deathAnimationDuration;

    protected MovementState movementState = MovementState.Grounded;

    protected bool isMoving = false;
    protected bool isGrounded { get => groundColliders.Count > 0; }
    protected bool isDead = false;

    protected Transform followTransform = null;
    protected List<Interactable> nearbyInteractables = new List<Interactable>();
    protected Interactable closestInteractable = null;

    private bool collidingInCurrentDirection = false;

    private List<Collider2D> groundColliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log(groundColliders.Count);
        //groundColliders.ForEach(coll => Debug.Log(coll.name));
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
        if(Input.GetButtonDown("Jump") && CanJump())
            Jump();

        // Interact
        if(Input.GetButtonDown("Interact") && closestInteractable != null)
            closestInteractable.OnInteract();

        // Swap Characters
        if(Input.GetButtonDown("Swap"))
            StartCoroutine(Swap());

        // Force-Death
        if(Input.GetButtonDown("Reset"))
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

        // If facing in direction of movement and colliding in current direction, return
        if(dir.x > 0 == transform.localScale.x > 0 && collidingInCurrentDirection)
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

        SetCollidingInCurrentDirection(false);
    }

    public virtual void Jump()
    {
        // ANIM JUMP
        anim.SetTrigger("Jump");
        jumpAudio.Play();
        //isGrounded = false;
        
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

        // Disable collider
        mainCollider.enabled = false;

        // Make sure camera is focused on this character
        if(other.IsCharacterActive())
            StartCoroutine(other.Swap());
        else if(!IsCharacterActive())
        {
            cameraController.SetActiveCharacter(this);
            cameraController.SetTarget(transform);
        }

        // Ground character
        SetMovementState(MovementState.Grounded);

        yield return null;

        while(!isGrounded)
        {
            SetMovementState(MovementState.Grounded);
            yield return null;
        }

        // Disable gravity
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        anim.SetTrigger("Die");

        yield return new WaitForSeconds(deathAnimationDuration);

        // Reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetMovementState(MovementState movementState)
    {
        this.movementState = movementState;

        // Reset velocity when switching to flying state
        if(movementState == MovementState.Flying)
            rb.velocity = Vector2.zero;
    }

    public void SetFollow(Transform target)
    {
        followTransform = target;
    }

    public void SetCollidingInCurrentDirection(bool value)
    {
        collidingInCurrentDirection = value;
    }

    protected virtual bool CanJump() => isGrounded;

    public bool IsCharacterActive() => cameraController.GetActiveCharacter() == this;

    public bool IsDead() => isDead;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ground Check
        if(isDead)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Ground") && !other.isTrigger)
                groundColliders.Add(other); //isGrounded = true;
        }
        else
        {
            if(!other.isTrigger)
                groundColliders.Add(other); //isGrounded = true;
        }

        // Interactables
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null && !nearbyInteractables.Contains(interactable))
            nearbyInteractables.Add(interactable);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Ground Check
        if(isDead)
        {
            if(other.gameObject.layer != LayerMask.NameToLayer("Ground") || other.isTrigger)
                groundColliders.Remove(other); //isGrounded = false;
        }
        else
        {
            if(!other.isTrigger && !groundColliders.Contains(other))
                groundColliders.Add(other); //isGrounded = true;
        }

        // Interactables
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
        {
            // If Interactable is closer than closestInteractable, set interactable as the closest
            if(closestInteractable == null 
                || Vector2.Distance(transform.position, interactable.transform.position)
                < Vector2.Distance(transform.position, closestInteractable.transform.position))
            {
                if(closestInteractable != null)
                    closestInteractable.SetHighlighted(false);

                closestInteractable = interactable;
                interactable.SetHighlighted(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Ground Check
        //if(!other.isTrigger)
        //    isGrounded = false;
        if(!other.isTrigger)
        {
            groundColliders.Remove(other);
        }

        // Interactables
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
        {
            if(closestInteractable == interactable)
            {
                closestInteractable = null;
                interactable.SetHighlighted(false);
            }

            nearbyInteractables.Remove(interactable);
        }
    }
}
