using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCollider : MonoBehaviour
{
    private List<Collider2D> connectedColliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveConnectedColliders(Vector2 delta)
    {
        return;

        foreach(Collider2D collider in connectedColliders)
            collider.transform.position += (Vector3)delta;
    }

    //public void MoveConnectedColliders(Vector2 delta)
    //{
    //    foreach(Collider2D collider in connectedColliders)
    //        collider.attachedRigidbody.velocity = delta;
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
            other.transform.SetParent(transform);

        //if(other.collider.CompareTag("Player")) //other.gameObject.layer != LayerMask.NameToLayer("Ground")
        //    connectedColliders.Add(other.collider);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))
            other.transform.SetParent(null);

        //if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        //{
        //    Debug.Log(other.collider.name);
        //    connectedColliders.Remove(other.collider);
        //    other.collider.attachedRigidbody.velocity = new Vector2(0, other.collider.attachedRigidbody.velocity.y);
        //}
    }
}
