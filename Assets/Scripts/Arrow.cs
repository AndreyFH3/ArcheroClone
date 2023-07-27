using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _blood;
    private int _damage;
    private Transform target;
    private bool shoot = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null) 
            Destroy(gameObject);
        if (shoot)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, target.position + Vector3.up, _speed * Time.fixedDeltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out EnemyHealth eh))
        {
            if (eh.GetDamage(_damage))
            {
                Destroy(eh.gameObject);
            }
            ParticleSystem ps = Instantiate(_blood, collision.contacts[0].point, Quaternion.EulerRotation(rb.velocity), collision.transform);
            ps.transform.LookAt(collision.contacts[0].point + rb.velocity);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out EnemyHealth eh))
        {
            if (eh.GetDamage(_damage))
            {
                Destroy(eh.gameObject);
            }
            ParticleSystem ps = Instantiate(_blood, other.transform.position, Quaternion.EulerRotation(rb.velocity), other.transform);
            ps.transform.LookAt(other.transform.position + rb.velocity);
        }
        Destroy(gameObject);
    }

    public void Shoot(Transform t, int damage)
    {
        target = t;
        _damage = damage;
        shoot = true;
    }
}
