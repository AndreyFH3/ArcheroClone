using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimationCurve;
    [SerializeField] private float _height;
    private float _timeToDestination;
    private Rigidbody rb;
    private Vector3 _targetPosition;
    private int damage;

    public void SetDamage(int dmg, float t)
    {
        damage = dmg;
        _timeToDestination = t;
    }
    public void SetDestination(Vector3 position)
    {
        rb = GetComponent<Rigidbody>();
        _targetPosition = position;
        rb.isKinematic = true;
        Destroy(gameObject,7.5f);
        StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        float timeToEnd = 0;
        Vector3 moveTo = transform.position;
        float localSpeed = (_targetPosition - moveTo).magnitude / _timeToDestination;
        while (timeToEnd < _timeToDestination)
        {
            timeToEnd += Time.fixedDeltaTime;
            moveTo = Vector3.MoveTowards(moveTo, _targetPosition, localSpeed * Time.fixedDeltaTime);

            float evolute = timeToEnd / _timeToDestination;

            rb.MovePosition(moveTo + new Vector3(0, _yAnimationCurve.Evaluate(evolute) * _height, 0));
            yield return Time.fixedDeltaTime;
        }
        rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerHealth ph))
        {
            ph.GetDamage(damage);
        }
        transform.SetParent(collision.transform);
        Destroy(gameObject);
    }
}
