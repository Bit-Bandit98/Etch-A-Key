// Property of Small-Ocelot https://github.com/Small-Ocelot . Cleaned up and modulated by Bit-Bandit https://github.com/Bit-Bandit98
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITransitionController : MonoBehaviour
{
    [SerializeField] private string LevelToLoad = "";
    private float transitionTime = 1f;
    private bool gameStarted;

    private bool canGoBackToTitle = false;

    public static event Action<float, bool> OnGameStart;


    private void Start()
    {
        OnGameStart?.Invoke(transitionTime, false);
        Invoke(nameof(EnableBackToTitle), transitionTime + 1f);
    }

    public void StartGamePerformed()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            OnGameStart?.Invoke(transitionTime, true);
            StartCoroutine(StartGameTransition());
        }
    }

    private IEnumerator StartGameTransition()
    {
        yield return new WaitForSeconds(transitionTime + .1f);
        SceneManager.LoadScene(LevelToLoad);
    }

    public void GoBackToTitle()
    {
        if (!canGoBackToTitle) return;
        canGoBackToTitle = false;
        OnGameStart?.Invoke(transitionTime, true);
        StartCoroutine(StartGameTransition());
    }

    private void EnableBackToTitle()
    {
        canGoBackToTitle = true;
    }

}