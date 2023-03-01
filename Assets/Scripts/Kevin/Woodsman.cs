using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodsman : Character
{
    //Audio 
    private AudioSource _audioSource;

    public Transform owlTransform;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    //audio 

  

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("space"));
    }

    public override void TestInput()
    {
        base.TestInput();

        // Press E while within 3 units of Owl to CallOwl
        if(Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, other.transform.position) < 3.0f)
            CallOwl();

        Move(new Vector2(Input.GetAxis("Horizontal"), 0));
    }

    public void CallOwl()
    {
        //other.transform.SetParent(owlTransform);

        //other.transform.localPosition = Vector3.zero;

        //Vector3 dir = (owlTransform.position - other.transform.position).normalized;
        //while(Vector2.Distance(other.transform.position, owlTransform.position) > 0.5f)
        //{
        //    other.transform.position += dir * Time.deltaTime;

        //    yield return null;
        //}

        (other as Owl).SetMovementState(MovementState.Flying);

        other.SetFollow(owlTransform);
    }


}
