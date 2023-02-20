using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CameraController cameraController;

    public Character other;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TestInput()
    {
        //Debug.Log(name);

        if(Input.GetKeyDown(KeyCode.Space))
            Swap();
    }

    public virtual void Swap()
    {
        cameraController.SetActiveCharacter(other);
        cameraController.SetTarget(other.transform);
    }

    public virtual void Move(Vector2 dir)
    {
        transform.position += (Vector3) dir.normalized * Time.deltaTime;
    }
}
