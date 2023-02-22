using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

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
        onActivate.Invoke();
    }

    public virtual void Deactivate()
    {
        onDeactivate.Invoke();
    }
}
