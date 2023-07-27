using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Wallet w))
        {
            w.AddCoin();
            Destroy(gameObject);
        }
    }
}
