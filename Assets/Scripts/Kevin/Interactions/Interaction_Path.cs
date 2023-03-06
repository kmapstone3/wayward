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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator OnInteract()
    {
        Debug.Log("INTERACTION PATH");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, layerMask);
        if(hit.collider != null && hit.collider.GetComponent<Interaction_Path>() != null)
            Debug.Log("Hit Next");

        line.SetPositions(new Vector3[] { transform.position, hit.point });

        yield return null;
    }
}
