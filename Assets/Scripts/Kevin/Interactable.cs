using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator anim;
    public Collider2D interactionArea;

    public Interaction action;

    [SerializeField] protected bool isActive;
    protected bool isHighlighted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
            return;

        CheckForMouse();

        // Left click when highlighted to interact
        if(isHighlighted && Input.GetMouseButtonDown(0))
            StartCoroutine(OnInteract());
    }

    public void CheckForMouse()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update highlighted status based on whether mouse is in interactionArea
        bool highlighted = interactionArea.bounds.Contains(mouseWorldPos);
        if(highlighted != isHighlighted)
            SetHighlighted(highlighted);
    }

    // Called when switching between highlighted states
    void SetHighlighted(bool value)
    {
        isHighlighted = value;

        anim.SetBool("Highlighted", value);
    }

    IEnumerator OnInteract()
    {
        isActive = false;

        // Wait for interaction to finish
        yield return action.OnInteract();

        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("k");
    }
}
