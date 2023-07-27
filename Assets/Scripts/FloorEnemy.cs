using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth))]
public class FloorEnemy : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeToDestination;
    [SerializeField] private Bullet _bullet;

    private float _timeToWalk;

    private Vector3 _startPosition;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _playerPosition;
    [SerializeField] private LayerMask _playerLayerMask;

    void Start()
    {
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();

        Collider[] cols = Physics.OverlapSphere(transform.position, 1000, _playerLayerMask);
        if (cols.Length > 0) 
            _playerPosition = cols[0].transform;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        GetComponent<EnemyHealth>().SetHealth(_health);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _startPosition) < _moveDistance)
        {
            Move();
        }
        else
        {
            _timeToWalk += Time.deltaTime;
            _animator.SetTrigger("Attack");
            if(_timeToWalk > _stayTime)
            {
                _timeToWalk = 0;
                _startPosition = transform.position;
            }
        }
    }
    public override void Attack()
    {
        Bullet b = Instantiate(_bullet, transform.position + Vector3.up, transform.rotation);
        b.SetDamage(_damage, _timeToDestination);
        b.SetDestination(_playerPosition.position);
    }

    public void SetShootSpeed()
    {
        _agent.isStopped = true;
        _animator.speed = _shootSpeed;
    }

    public void SetShootSpeedBase()
    {
        _agent.isStopped = false;
        _animator.speed = 1;
    }
    public override void Move()
    {
        _animator.SetBool("IsWalk", true);
        _agent.SetDestination(_playerPosition.position);
    }
}
