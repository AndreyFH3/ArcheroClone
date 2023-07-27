using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health
{
    public void RegisterEvent(UnityAction act) => _onDead.AddListener(act);
    
    public void UnregisterEvent(UnityAction act) =>_onDead.RemoveListener(act);
    
    public void UnregisterAllEvents() => _onDead.RemoveAllListeners();
}
