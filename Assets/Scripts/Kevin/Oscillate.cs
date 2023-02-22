using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Oscillate : MonoBehaviour
{
    public Axis axis;

    public float frequency;
    public float amplitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float y = 0;

        if(axis.HasFlag(Axis.X))
            x = Mathf.Sin(Time.time * frequency) * amplitude;
        if(axis.HasFlag(Axis.Y))
            y = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.localPosition = new Vector2(x, y);
    }
}
