using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Movement : MonoBehaviour
{
    public float speed = 1;
    
    private Transform _transform;
    private Vector3 _destination;
    private BaseCreature _creatureToMove;
    private Vector3 _startPosition;

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
        else if (_creatureToMove.state == State.MovingBack) // aliens only
        {
            float step = speed * Time.fixedDeltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, _startPosition, step);
            //if the alien reached their spaceship with a cow
        }
    }

    public void SetDestination(Vector3 endPosition)
    {
        _destination = new Vector3(endPosition.x, _startPosition.y);
    }
    
    public void SetStartPosition(Vector3 startPosition)
    {
        _startPosition = startPosition;
    }
}
