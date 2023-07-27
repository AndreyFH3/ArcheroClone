using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(EnemyHealth))] 
public class FlyEnemy : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeToDestination;

    private Rigidbody _rb;

    private Vector3 _startPosition;

    private Transform _playerPosition;
    [SerializeField] private LayerMask _playerLayerMask;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        GetComponent<EnemyHealth>().SetHealth(_health);
        _startPosition = transform.position;

        Collider[] cols = Physics.OverlapSphere(transform.position, 1000, _playerLayerMask);
        if (cols.Length > 0)
            _playerPosition = cols[0].transform;
        Move();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 4);
    }

    private IEnumerator Fly()
    {
        while(Vector3.Distance(transform.position, _startPosition) < _moveDistance)
        {
            _rb.velocity = (transform.position - _playerPosition.position).normalized * -_speed * Time.fixedDeltaTime;
            
            yield return Time.fixedDeltaTime;
        }
        Attack();
    }

    private IEnumerator AttackFly()
    {
        yield return new WaitForSeconds(_stayTime);
        _rb.velocity = (transform.position - _playerPosition.position).normalized * -_shootSpeed * Time.fixedDeltaTime;
        _rb.mass = 5;
        yield return new WaitForSeconds(_timeToDestination);
        _rb.mass = 1;
        _startPosition = transform.position;
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out PlayerHealth ph))
        {
            ph.GetDamage(_damage);
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackFly());
    }

    public override void Move()
    {
        StartCoroutine(Fly());
    }
}