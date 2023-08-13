using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform _head;
    public Vector3 targetPosition { get; private set; } = Vector3.zero;
    private float _velocityMagnitude = 0f;
    private Vector3 _scale;
    private void Start()
    {
        targetPosition = transform.position;
        _scale = transform.localScale;
    }

    private void Update()
    {
        if (_velocityMagnitude > 0.1f)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,targetPosition,maxDistance);
        }
        else
        {
            transform.position = targetPosition;
        }

        transform.localScale = _scale;
    }

    public void SetSpeed(float value) => _speed = value;

    public void SetMovement(in Vector3 position,in Vector3 velocity, in Vector3 scale, in float averageInterval)
    {
        targetPosition = position + (velocity * averageInterval);
        _velocityMagnitude = velocity.magnitude;
        this._velocity = velocity;
        _scale = scale;
    }

    public void SetRotateX(float value)
    {
        _head.localEulerAngles = new Vector3(value,0,0);
    }

    public void SetRoateY(float value)
    {
        transform.localEulerAngles = new Vector3(0, value, 0);
    }
    
}
