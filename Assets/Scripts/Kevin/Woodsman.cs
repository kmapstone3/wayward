using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodsman : Character
{
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

        Move(new Vector2(Input.GetAxis("Horizontal"), 0));
    }
}
