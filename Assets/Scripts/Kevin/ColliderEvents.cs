using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public List<string> tags;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.isTrigger && (tags.Contains(other.tag) || tags.Count == 0))
            onTriggerEnter.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!other.isTrigger && (tags.Contains(other.tag) || tags.Count == 0))
            onTriggerStay.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.isTrigger && (tags.Contains(other.tag) || tags.Count == 0))
            onTriggerExit.Invoke();
    }
}
