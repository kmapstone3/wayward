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

    // Called after starting animation by other object
    public void Initialize()
    {
        Character defaultCharacter = FindObjectOfType<Woodsman>();

        SetTarget(defaultCharacter.transform);
        SetActiveCharacter(defaultCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        if(activeCharacter != null && !activeCharacter.IsDead())
            activeCharacter.TestInput();

        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        if(target == null)
            return;

        //if(Vector2.Distance(transform.position, target.position) < 0.5f)
            //return;

        Vector2 delta = (target.position - transform.position) * cameraSpeed * Time.deltaTime;
        transform.position += (Vector3) delta;
    }

    public Character GetActiveCharacter() => activeCharacter;

    public void SetActiveCharacter(Character character)
    {
        activeCharacter = character;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
