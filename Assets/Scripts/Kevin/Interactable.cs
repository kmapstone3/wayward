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

    protected Character character = null;
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

        if(character != null)
            SetIsCharacterNearby(character.IsCharacterActive());
        else
            SetIsCharacterNearby(false);

        CheckForMouse();

        // Left click when highlighted to interact
        if(isHighlighted && Input.GetMouseButtonDown(0))
            StartCoroutine(OnInteract());
    }

    public void CheckForMouse()
    {
        if(!isCharacterNearby || character == null)
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

    IEnumerator OnInteract()
    {
        isActive = false;

        anim.SetTrigger("Interact");

        // Wait for interaction to finish
        yield return action.OnInteract();

        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer(characterType.ToString()) || characterType == CharacterType.Both)
        //{

        //}

        if(other.CompareTag("Player"))
            character = other.GetComponent<Character>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            character = null;
    }
}
