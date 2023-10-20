using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    private Animator _animator;
    public Transform targetPoint
    {
        get { return _targetPoint; }
    }
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _targetPoint = _endPoint;
    }

    void FixedUpdate()
    {
        if (_targetPoint != null)
            Walk();

    }

    void Walk()
    {
        Vector2 direction = _targetPoint.position - transform.position;
        float distanceToTarget = direction.magnitude;
        if (distanceToTarget > 0.1f)
        {
            Vector2 moveDirection = direction.normalized * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + moveDirection);
        }
        else
        {
            _targetPoint = null;
        }
    }

    public void GoBack()
    {
        _targetPoint = _startPoint;
        _animator.SetBool("Left", true);
    }


}
