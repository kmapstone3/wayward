using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interaction_Move : Interaction
{
    public Rigidbody2D rb;

    public Transform directionTransform;
    public Hazard hazardObject;

    public MoveWithCollider rideTransform;

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

    // Movement using Rigidbody2D
    //public override IEnumerator OnInteract()
    //{
    //    Vector2 initialVelocity = rb.velocity;

    //    Vector2 dir = directionTransform.right;

    //    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    //    if (Mathf.Abs(dir.x) > 0.5f)
    //        rb.constraints += (int)RigidbodyConstraints2D.FreezePositionY;
    //    if (Mathf.Abs(dir.y) > 0.5f)
    //        rb.constraints += (int)RigidbodyConstraints2D.FreezePositionX;

    //    hazardObject.gameObject.SetActive(true);

    //    float startTime = Time.time;
    //    while (Time.time < startTime + duration)
    //    {
    //        if (colliding)
    //            break;

    //        Vector3 delta = (Vector3)(dir * speed * Time.deltaTime);
    //        rb.velocity = dir * speed;
    //        if (rideTransform != null)
    //            rideTransform.MoveConnectedColliders(delta);
    //        yield return null;
    //    }

    //    rb.velocity = initialVelocity;

    //    rb.constraints = RigidbodyConstraints2D.FreezeAll;

    //    hazardObject.gameObject.SetActive(false);

    //    transform.position = Utils.SnapPosition(transform.position);

    //    directionAngle += -90 * directionRotation;
    //    directionTransform.eulerAngles = new Vector3(0, 0, directionAngle);
    //}

    public override IEnumerator OnInteract() // Movement using Transform
    {
        Vector2 initialVelocity = rb.velocity;

        Vector2 dir = directionTransform.right;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        hazardObject.gameObject.SetActive(true);

        float startTime = Time.time;
        while(Time.time < startTime + duration)
        {
            if(colliding)
                break;

            Vector3 delta = (Vector3)(dir * speed * Time.deltaTime);
            transform.position += delta;
            if (rideTransform != null)
                rideTransform.MoveConnectedColliders(delta);
            yield return null;
        }

        rb.velocity = initialVelocity;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        hazardObject.gameObject.SetActive(false);

        transform.position = Utils.SnapPosition(transform.position);

        float angleDelta = -90 * directionRotation;

        for(int i = 0; i < Mathf.Abs(angleDelta); i++)
        {
            directionTransform.eulerAngles += new Vector3(0, 0, -1 * Utils.Polarize(directionRotation));

            yield return null;
        }

        directionAngle += angleDelta;
        directionTransform.eulerAngles = new Vector3(0, 0, directionAngle);
    }

    public void SetCollide(bool value)
    {
        colliding = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colliding = true;

        rb.velocity = Vector2.zero;
    }
}
