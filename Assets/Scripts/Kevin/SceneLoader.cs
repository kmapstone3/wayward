using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public CameraController cameraController;

    public Animator openingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initialization());
    }

    IEnumerator Initialization()
    {
        yield return null;

        //openingAnimator.Play("");

        Debug.Log(openingAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(openingAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Initialize cameraController
        cameraController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
