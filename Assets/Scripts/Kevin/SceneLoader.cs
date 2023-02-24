using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public CameraController cameraController;

    public Animator openingAnimator;

    public int openingAnimationDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initialization());
    }

    IEnumerator Initialization()
    {
        //openingAnimator.Play("");

        //Debug.Log(openingAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(openingAnimationDuration);

        // Initialize cameraController
        cameraController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
