// Property of Small-Ocelot https://github.com/Small-Ocelot

using System.Collections;
using UnityEngine;

public class UI_Transitions : MonoBehaviour {

    [SerializeField] private CanvasGroup blackBackgroundCG;

    private float transitionTime;


    private void OnSceneTransitionTrigger(float transitionTime, bool fadeToBlack) {
        this.transitionTime = transitionTime;
        StartCoroutine(fadeToBlack ? BlackFadeIn() : BlackFadeAway());
    }

    public IEnumerator BlackFadeAway() {
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime) {
            elapsedTime += Time.deltaTime;
            float fadeAmount = elapsedTime / transitionTime;
            float newAlpha = Mathf.Lerp(1f, 0f, fadeAmount);
            blackBackgroundCG.alpha = newAlpha;
            yield return null;
        }
        blackBackgroundCG.alpha = 0f;
    }

    public IEnumerator BlackFadeIn() {
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime) {
            elapsedTime += Time.unscaledDeltaTime;
            float fadeAmount = elapsedTime / transitionTime;
            float newAlpha = Mathf.Lerp(0f, 1f, fadeAmount);
            blackBackgroundCG.alpha = newAlpha;
            yield return null;
        }
        blackBackgroundCG.alpha = 1f;
    }

    private void OnEnable()
    {
        UITransitionController.OnGameStart += OnSceneTransitionTrigger;
    }

    private void OnDisable() {
        UITransitionController.OnGameStart -= OnSceneTransitionTrigger;
    }

}
