using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Button _setPause;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _pauseMenu;
    [SerializeField] private Button _loseMenu;
    [SerializeField] private Button _win;
    [SerializeField] private Button _winMenu;



    [SerializeField] private GameObject _playMenu;

    [SerializeField] private int _nextLevel;

    private void OnEnable()
    {
        _continue.onClick.AddListener(
            delegate
            {
                _playMenu.SetActive(false);
                Time.timeScale = 1;
            });
        _setPause.onClick.AddListener(
            delegate
            {
                _playMenu.SetActive(true);
                Time.timeScale = 0;
            });

        _restart.onClick.AddListener(()=>LoadingLevel(SceneManager.GetActiveScene().buildIndex));
        _pauseMenu.onClick.AddListener(()=>LoadingLevel(0));
        _loseMenu.onClick.AddListener(()=>LoadingLevel(0));
        _win.onClick.AddListener(()=>LoadingLevel(_nextLevel));
        _winMenu.onClick.AddListener(()=>LoadingLevel(0));
    }

    private void OnDisable()
    {
        _setPause.onClick.RemoveAllListeners();
        _continue.onClick.RemoveAllListeners();
        _restart.onClick.RemoveAllListeners();
        _pauseMenu.onClick.RemoveAllListeners();
        _loseMenu.onClick.RemoveAllListeners();
        _win.onClick.RemoveAllListeners();
        _winMenu.onClick.RemoveAllListeners();
    }

    private void LoadingLevel(int index)
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Level", index);
    }
}
