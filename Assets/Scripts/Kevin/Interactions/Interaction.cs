using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator OnInteract();

    protected IEnumerator WaitForDuration()
    {
        if(duration <= 0)
            yield break;

        yield return new WaitForSeconds(duration);
    }
}
