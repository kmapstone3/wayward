using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Path : Interaction
{
    public LayerMask layerMask;

    public Vector2 dir;
    public float distance;

    public Interaction_Path next;

    public LineRenderer line;

    public Transform originTransform;
    public Transform receivingTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by Interactable or when receiving raycast
    public override IEnumerator OnInteract()
    {
        // Set active

        Debug.Log("INTERACTION PATH");
        RaycastHit2D hit = Physics2D.Raycast(originTransform.position, dir, distance, layerMask);
        if(hit.collider != null && hit.collider.GetComponent<Interaction_Path>() != null)
            ActivateNext(hit.collider.GetComponent<Interaction_Path>());

        Debug.Log(transform.position + "," + hit.point);
        line.SetPositions(new Vector3[] { originTransform.position, hit.point });

        yield return null;
    }

    void ActivateNext(Interaction_Path next)
    {

    }
}
