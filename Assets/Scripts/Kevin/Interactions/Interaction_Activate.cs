using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Activate : Interaction
{
    public GameObject activatedObject;
    public SpriteRenderer dimmingRenderer;

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
        yield return new WaitForSeconds(1.0f);

        for(int i = 0; i < 200; i++)
        {
            if(activatedObject.activeSelf)
                dimmingRenderer.color -= new Color(0, 0, 0, 0.005f);
            else
                dimmingRenderer.color += new Color(0, 0, 0, 0.005f);

            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        activatedObject.SetActive(!activatedObject.activeSelf);
    }
}
