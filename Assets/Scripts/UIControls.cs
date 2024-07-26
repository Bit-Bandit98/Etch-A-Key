using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIControls : MonoBehaviour
{
    private EtchActions Controls;
    private float transitionTime = 1f;
    private bool gameStarted;

    public static event Action<float, bool> OnGameStart;

    private void Awake()
    {
        Invoke(nameof(Initialise), transitionTime);

    }

    private void Start() {
        OnGameStart?.Invoke(transitionTime, false);
    }

    private void Initialise()
    {
        Controls = new EtchActions();
        Controls.Enable();
        Controls.UI.StartGame.performed += ctx => StartGamePerformed(); 
    }

    private void StartGamePerformed() {
        if (!gameStarted) {
            gameStarted = true;
            OnGameStart?.Invoke(transitionTime, true);
            StartCoroutine(StartGameTransition());
        }
    }

    private IEnumerator StartGameTransition() {
            yield return new WaitForSeconds(transitionTime + .1f);
            SceneManager.LoadScene("Canvas");
    }

    private void OnEnable()
    {
        //Controls.Enable();
    }


    private void OnDisable()
    {
        Controls.Disable();
    }
}
