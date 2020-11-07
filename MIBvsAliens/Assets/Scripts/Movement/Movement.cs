using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Movement : MonoBehaviour
{
    public float speed = 1;
    
    private Transform _transform;
    private Vector3 _destination;
    private BaseCreature _creatureToMove;

    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _creatureToMove = gameObject.GetComponent<BaseCreature>();
    }
    
    void FixedUpdate()
    {
        if (_creatureToMove.state == State.Moving)
        {
            float step = speed * Time.fixedDeltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, _destination, step);
        }
    }

    public void SetDestination(Vector3 endPosition)
    {
        _destination = endPosition;
    }
}
