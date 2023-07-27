using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    [SerializeField] private Canvas _deadCanvas;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthThext;

    private void Awake()
    {
        _onDead.AddListener(ShowUI);
    }
    
    public new void SetHealth(int health)
    {
        _maxHealth = _health = health;
        _healthSlider.maxValue = _maxHealth;

        _healthSlider.value = _health;
        _healthThext.text = $"{_health}/{_maxHealth}";

        _healthSlider.gameObject.SetActive(true);
    }

    public new bool GetDamage(int damage)
    {
        _health -= damage;
        _healthSlider.value = _health;
        _healthThext.text = $"{_health}/{_maxHealth}";
        if (_health <= 0)
        {

            _onDead.Invoke();
            _onDead.RemoveAllListeners();
            return true;
        }
        return false;
    }

    private void ShowUI()
    {
        _deadCanvas.gameObject.SetActive(true);
    }
}
