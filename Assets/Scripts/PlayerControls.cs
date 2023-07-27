using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerControls : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _health = 100;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private int _damage = 75;
    [Header("Controls")]
    [SerializeField] private Joystick _joystick;
    [Header("Arrow")]
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Transform _arrowSpawnPosition;
    [SerializeField] private LayerMask _enemyLayerMask;
    private CharacterController _characterController;
    private Animator _animator;
    private Transform _target;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        GetComponent<PlayerHealth>().SetHealth(_health);
    }

    private void Update()
    {
        if(_joystick.Direction.magnitude == 0)
        {
            if (_target == null)
            { 
                FindTarget();
            }
            else
            {
                Shoot();
            }
        }
        else
        {
            _target = null;
        }
        Movement(_joystick.Direction);
    }

    public void GenerateArrow()
    {
        if (_target == null) return;

        Arrow ar = Instantiate(_arrow, _arrowSpawnPosition.position, _arrowSpawnPosition.rotation);
        ar.Shoot(_target, _damage);
    }

    private void Shoot()
    {
        _animator.SetBool("IsShooting", true);
        
        Vector3 direction = new Vector3(_target.position.x - transform.position.x, 0, _target.position.z - transform.position.z);
        direction.Normalize();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);
    }

    private void FindTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 1000, _enemyLayerMask);
        if(cols.Length > 0)
        {
            _target = cols[0].transform;
            foreach (Collider col in cols)
            {
                if(Vector3.Distance(transform.position, _target.position) > Vector3.Distance(transform.position, col.transform.position))
                {
                    _target = col.transform;
                }
            }
        }
    }

    public void Movement(Vector2 dir)
    {
        if (dir.magnitude == 0)
        {
            _animator.SetBool("IsMove", false);
            return;
        }
        
        _animator.SetBool("IsMove", true);
        _animator.SetBool("IsShooting", false);

        Vector3 direction = new Vector3(dir.x, 0, dir.y);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);

        _characterController.Move(transform.TransformDirection(Vector3.forward * _speed * Time.deltaTime));
    }
}
