using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _loadingText;

    private bool _isLoadingEnd = false;
    private bool _loadNext = false;

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }


    private IEnumerator LoadScene()
    {
        Time.timeScale = 1.0f;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Level", 0));
        while(!asyncOp.isDone) 
        {
            float progress = asyncOp.progress / .9f;
            _slider.value = progress;
            _loadingText.text = $"{progress * 100}%";
            yield return null;
        }
    }
}