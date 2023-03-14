using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    protected bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate()
    {
        Debug.Log(name + " Activate");
        onActivate.Invoke();

        isActivated = true;
    }

    public virtual void Deactivate()
    {
        onDeactivate.Invoke();

        isActivated = false;
    }
}
