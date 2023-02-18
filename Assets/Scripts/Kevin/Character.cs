using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CameraController cameraController;

    public Character other;

    [SerializeField] private bool controlsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!controlsActive)
            return;

        TestInput();
    }

    public virtual void TestInput()
    {
        //Debug.Log(name);

        if(Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(OnSwap());
    }

    public virtual IEnumerator OnSwap()
    {
        // Disable this character's controls
        DisableControls();
        
        // Wait one frame so both characters aren't active
        yield return null;
        
        // Enable other character's controls
        other.EnableControls();
    }

    public void EnableControls()
    {
        cameraController.SetTarget(transform);

        controlsActive = true;
    }

    public void DisableControls()
    {
        controlsActive = false;
    }

    public virtual void Move(Vector2 dir)
    {
        transform.position += (Vector3) dir.normalized * Time.deltaTime;
    }
}
