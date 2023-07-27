using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _drop;
    private int _enemiesAmount;

    [Header("Three Seconds Timer Before Start")]
    [SerializeField] private Image _circleImage;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private GameObject _timerObject;
    [SerializeField] private PlayerControls _playerControls;

    List<Enemy> enemies = new List<Enemy>();

    [Header("onwin")]
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    private void Awake()
    {
        _enemiesAmount = _spawnPoints.Length;
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < _enemiesAmount; i++)
        {
            Enemy e =Instantiate(_enemies[Random.Range(0, _enemies.Length)], 
                _spawnPoints[i].position,
                Quaternion.identity);

            e.GetComponent<EnemyHealth>().RegisterEvent(OnDeadEnemy);
            e.GetComponent<EnemyHealth>().RegisterEvent(() => MoneyDrop(e.transform));
            enemies.Add(e);
        }
        StartCoroutine(ThreeSecondTimer());
    }

    private void OnDeadEnemy()
    {
        _enemiesAmount--;
        if( _enemiesAmount <= 0)
        {
            _start.gameObject.SetActive(false);
            _end.gameObject.SetActive(true);
        }
    }

    private void MoneyDrop(Transform pos)
    {
        GameObject d = Instantiate(_drop, pos.position, pos.rotation);
    }

    private IEnumerator ThreeSecondTimer()
    {
        float threeSecond = 3f;
        while (threeSecond > 0)
        {
            threeSecond -= Time.deltaTime;
            _textTimer.text = $"{Mathf.RoundToInt(threeSecond)}";
            _circleImage.fillAmount = threeSecond / 3.0f;
            yield return null;
        }
        foreach (Enemy e in enemies)
        {
            e.enabled = true;
        }
        _timerObject.SetActive(false);
        _playerControls.enabled = true;
    }
}
