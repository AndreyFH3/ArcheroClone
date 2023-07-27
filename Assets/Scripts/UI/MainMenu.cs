using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _LevelToLoad;

    private void OnEnable()
    {
        _button?.onClick.AddListener(LoadinScene);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(LoadinScene);
    }

    private void LoadinScene()
    {
        PlayerPrefs.SetInt("Level", _LevelToLoad);
        SceneManager.LoadScene(1);
    }
}
