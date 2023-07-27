using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWin : MonoBehaviour
{
    [SerializeField] private Canvas _winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out PlayerControls pc))
        {
            _winCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
