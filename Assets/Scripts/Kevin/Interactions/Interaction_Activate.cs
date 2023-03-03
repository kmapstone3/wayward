using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Activate : Interaction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator OnInteract()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        yield return null;
    }
}
