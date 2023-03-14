using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_ObjectInArea : Trigger
{
    public GameObject targetObject;

    private bool inArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivationDelay()
    {
        if(inArea)
            yield break;

        inArea = true;

        //float startTime = Time.time;
        //while(Time.time < startTime + 1)
        //{
        //    yield return null;
        //}

        yield return new WaitForSeconds(1.0f);

        if(inArea)
            Activate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == targetObject)
            StartCoroutine(ActivationDelay());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == targetObject)
        {
            inArea = false;
            
            if(isActivated)
                Deactivate();
        }
    }
}
