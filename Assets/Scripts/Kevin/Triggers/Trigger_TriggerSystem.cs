using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_TriggerSystem : Trigger
{
    public bool[] slots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForActivation();
    }

    void CheckForActivation()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(!slots[i])
                return;
        }

        Debug.Log("k");
        Activate();
    }

    public void ActivateSlot(int slot)
    {
        slots[slot] = true;
    }

    public void DeactivateSlot(int slot)
    {
        slots[slot] = false;
    }
}
