using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interaction_Move : Interaction
{
    public Rigidbody2D rb;

    public Transform directionTransform;

    [Tooltip("The amount of direction rotation after each use of this object (1 = 90 degrees clockwise).")]
    public int directionRotation;

    public float speed;

    [SerializeField] private float directionAngle;

    private bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator OnInteract()
    {
        Vector2 initialVelocity = rb.velocity;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //rb.bodyType = RigidbodyType2D.Dynamic;

        float startTime = Time.time;
        while(Time.time < startTime + duration)
        {
            if(colliding)
                break;

            rb.velocity = directionTransform.right * speed; //new Vector2(velocity.x, rb.velocity.y);
            yield return null;
        }

        rb.velocity = initialVelocity;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.bodyType = RigidbodyType2D.Static;

        transform.position = new Vector2(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2);

        directionAngle += -90 * directionRotation;
        directionTransform.eulerAngles = new Vector3(0, 0, directionAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colliding = true;

        rb.velocity = Vector2.zero;
    }
}
