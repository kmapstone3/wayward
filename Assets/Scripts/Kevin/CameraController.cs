using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;

    [SerializeField] private Transform target;
    [SerializeField] private Character activeCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeCharacter.TestInput();

        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        if(target == null)
            return;

        Vector2 delta = (target.position - transform.position).normalized * cameraSpeed * Time.deltaTime;
        transform.position += (Vector3) delta;
    }

    public void SetActiveCharacter(Character character)
    {
        activeCharacter = character;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
