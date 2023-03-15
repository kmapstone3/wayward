using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //Audio
    public  AudioSource audioSource;

    public Animator anim;

    public Collider2D exitCollider;

    [Tooltip("The transform both characters move towards when leaving the scene.")]
    public Transform exitTransform;

    public float openAnimationDuration;

    [SerializeField] private bool isOpen;
    private bool busy = false;

    private List<Character> playersInRange = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FocusCamera(CameraController camera) => StartCoroutine(FocusCameraCo(camera));

    IEnumerator FocusCameraCo(CameraController camera)
    {
        if(busy)
            yield break;
        
        busy = true;

        camera.DisableInteraction();

        yield return new WaitForSeconds(1.0f);

        // Remember last active character
        Character character = camera.GetActiveCharacter();

        // Set Camera Target
        camera.SetTarget(transform);
        
        // Open Door
        if(!isOpen)
            anim.SetTrigger("Open");
        else
            anim.SetTrigger("Close");
        audioSource.Play();

        isOpen = !isOpen;

        // Wait for animation to finish
        yield return new WaitForSeconds(openAnimationDuration);

        // Set Camera Target to last active character
        camera.SetTarget(character.transform);

        camera.EnableInteraction();

        busy = false;
    }

    IEnumerator ExitSequence()
    {
        exitCollider.gameObject.SetActive(false);

        Woodsman woodsman = null;
        Owl owl = null;

        // Identify Woodsman and Owl
        foreach(Character character in playersInRange)
            if(character is Woodsman)
                woodsman = character as Woodsman;
            else if(character is Owl)
                owl = character as Owl;

        CameraController camera = woodsman.cameraController;

        // Unfocus camera
        camera.SetTarget(null);
        camera.SetActiveCharacter(null);

        // Set characters to walk out to exitTransform
        woodsman.CallOwl();
        woodsman.SetFollow(exitTransform);

        // Wait for exit duration
        yield return new WaitForSeconds(3.0f);

        // Load next scene
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !playersInRange.Contains(other.GetComponent<Character>()))
            playersInRange.Add(other.GetComponent<Character>());

        if(playersInRange.Count >= 2)
            StartCoroutine(ExitSequence());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && playersInRange.Contains(other.GetComponent<Character>()))
            playersInRange.Remove(other.GetComponent<Character>());
    }
}
