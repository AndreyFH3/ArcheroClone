using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move();

    public abstract void Attack();

}
