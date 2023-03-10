using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Interaction_Path : Interaction
{
    public LayerMask layerMask;

    public Vector2 dir;
    public float distance;

    public Interactable interactable;
    public Trigger_Script trigger;

    public SpriteRenderer activeFill;
    public LineRenderer line;

    public Transform originTransform;
    public Transform receivingTransform;

    private bool connected = false;

    private Interaction_Path next = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(connected)
        {
            RaycastHit2D hit = Physics2D.Raycast(originTransform.position, dir, distance, layerMask);
            bool success = IsHitSuccess(hit);
            connected = success;

            if(!success)
                StartCoroutine(BreakLine());
        }
    }

    // Called by Interactable or when receiving raycast
    public override IEnumerator OnInteract()
    {
        if(connected)
            yield break;

        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(originTransform.position, dir, distance, layerMask);
        bool success = IsHitSuccess(hit);
        connected = success;

        yield return ProjectLine(hit.point, success);

        if(success)
        {
            next = hit.transform.parent.GetComponent<Interaction_Path>();
            next.Activate();
            interactable.SetActive(false);
        }

        yield return null;
    }

    IEnumerator ProjectLine(Vector2 targetPos, bool success)
    {
        line.SetPositions(new Vector3[] { originTransform.position, originTransform.position });

        while(Vector2.Distance(line.GetPosition(1), targetPos) > 0.1f)
        {
            line.SetPositions(new Vector3[] { originTransform.position, (Vector2) line.GetPosition(1) + dir.normalized * 0.02f });
            
            yield return null;
        }

        line.SetPositions(new Vector3[] { originTransform.position, targetPos });

        if(!success)
            yield return BreakLine();

        yield return null;
    }

    IEnumerator BreakLine()
    {
        connected = false;

        if(next != null)
            next.Deactivate();

        while(line.endColor.a > 0)
        {
            line.startColor -= new Color(0, 0, 0, 0.005f);
            line.endColor -= new Color(0, 0, 0, 0.005f);

            yield return null;
        }

        line.SetPositions(new Vector3[] { originTransform.position, originTransform.position });
        line.startColor += new Color(0, 0, 0, 1);
        line.endColor += new Color(0, 0, 0, 1);
    }

    public void Activate()
    {
        activeFill.gameObject.SetActive(true);

        if(interactable != null)
            interactable.SetActive(true);

        if(trigger != null)
            trigger.Activate();
    }

    public void Deactivate()
    {
        activeFill.gameObject.SetActive(false);

        if(interactable != null)
            interactable.SetActive(false);

        if(trigger != null)
            trigger.Deactivate();

        StartCoroutine(BreakLine());
    }

    bool IsHitSuccess(RaycastHit2D hit) => 
        hit.collider != null && hit.transform.parent != null && hit.transform.parent.GetComponent<Interaction_Path>() != null;
}
