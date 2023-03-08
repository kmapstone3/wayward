using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator anim;
    [Tooltip("The area the mouse must be within to select this Interactable "
        + "(Character region is specified by trigger collider attached to this GameObject).")]
    public Collider2D interactionArea;
    public CharacterType characterType;

    public Interaction action;

    [SerializeField] protected bool isActive;
    protected bool isCharacterNearby = false;
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

        //CheckForMouse();

        // Left click when highlighted to interact
        if(isHighlighted && Input.GetMouseButtonDown(0))
            StartCoroutine(OnInteractCo());
    }

    public void CheckForMouse()
    {
        if(!isCharacterNearby)
        {
            isHighlighted = false;
            return;
        }

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update highlighted status based on whether mouse is in interactionArea
        bool highlighted = interactionArea.bounds.Contains(mouseWorldPos);
        if(highlighted != isHighlighted)
            SetHighlighted(highlighted);
    }

    void SetIsCharacterNearby(bool value)
    {
        isCharacterNearby = value;

        anim.SetBool("In Range", value);
    }

    // Called when switching between highlighted states
    void SetHighlighted(bool value)
    {
        isHighlighted = value;

        anim.SetBool("Highlighted", value);
    }

    public void OnInteract()
    {
        if(!isActive)
            return;

        StartCoroutine(OnInteractCo());
    }

    IEnumerator OnInteractCo()
    {
        isActive = false;

        anim.SetTrigger("Interact");

        // Wait for interaction to finish
        yield return action.OnInteract();

        isActive = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            SetIsCharacterNearby(other.GetComponent<Character>().IsCharacterActive());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            SetIsCharacterNearby(false);
    }
}
