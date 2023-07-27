using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _maxPosition;
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _followObject;
    private Vector3 _distanceToObject;
    public Vector3 MaxPosition { get => _maxPosition.position; }
    public Vector3 MinPosition { get => _minPosition.position; }

    private void Start()
    {
        _distanceToObject = transform.position - _followObject.position;
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y, 
            _followObject.position.z + _distanceToObject.z);

        if(targetPosition.z < MaxPosition.z && targetPosition.z > MinPosition.z)
        {
            transform.position = targetPosition;
        }
    }
}
