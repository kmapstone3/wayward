using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodsman : Character
{
    public Transform owlTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TestInput()
    {
        base.TestInput();

        //if(Input.GetKeyDown(KeyCode.Q))
            //PickUpOwl();

        Move(new Vector2(Input.GetAxis("Horizontal"), 0));
    }

    public void PickUpOwl()
    {
        other.transform.SetParent(owlTransform);

        other.transform.localPosition = Vector3.zero;
    }
}
