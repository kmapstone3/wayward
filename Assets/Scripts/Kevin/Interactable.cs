using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Interactable : MonoBehaviour
{
    public Animator anim;
    [Tooltip("The area the mouse must be within to select this Interactable "
        + "(Character region is specified by trigger collider attached to this GameObject).")]
    public Collider2D interactionArea;
    public CharacterType characterType;

    public Interaction action;
    public List<Interaction> actions;

    public AudioSource activationAudio;

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
        //if(!isActive)
            //return;

        //CheckForMouse();

        // Left click when highlighted to interact
        //if(isHighlighted && Input.GetMouseButtonDown(0))
            //
            //StartCoroutine(OnInteractCo());
    }

    //public void CheckForMouse()
    //{
    //    if(!isCharacterNearby)
    //    {
    //        isHighlighted = false;
    //        return;
    //    }

    //    Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    // Update highlighted status based on whether mouse is in interactionArea
    //    bool highlighted = interactionArea.bounds.Contains(mouseWorldPos);
    //    if(highlighted != isHighlighted)
    //        SetHighlighted(highlighted);
    //}

    public void SetActive(bool value)
    {
        isActive = value;

        anim.SetBool("In Range", CanBeUsed());
    }

    void SetIsCharacterNearby(bool value)
    {
        isCharacterNearby = value;

        anim.SetBool("In Range", CanBeUsed());
    }

    // Called when switching between highlighted states
    public void SetHighlighted(bool value)
    {
        // Don't highlight if not active
        if(value && !isActive)
            return;

        isHighlighted = value;

        anim.SetBool("In Range", CanBeUsed());
    }

    public void OnInteract()
    {
        if(!CanBeUsed())
            return;

        StartCoroutine(OnInteractCo());
    }

    IEnumerator OnInteractCo()
    {
        SetActive(false);

        anim.SetTrigger("Interact");
        activationAudio.Play();

        CameraController cam = FindObjectOfType<CameraController>();

        Character activeCharacter = cam.GetActiveCharacter();
        Transform target = cam.GetTarget();

        cam.DisableInteraction();

        foreach(Interaction action in actions)
        {
            //activeCharacter.ReleaseCameraControl();
            cam.SetTarget(action.transform);

            // Wait for interaction to finish
            yield return action.OnInteract();
        }

        while(cam.GetActiveCharacter() == null)
            yield return null;

        //cam.SetActiveCharacter(activeCharacter);
        cam.SetTarget(cam.GetActiveCharacter().transform);
        cam.EnableInteraction();

        SetActive(true);
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

    bool CanBeUsed() => isActive && isHighlighted && FindObjectOfType<CameraController>().IsInteractionEnabled(); // Previously included isCharacterNearby
}
