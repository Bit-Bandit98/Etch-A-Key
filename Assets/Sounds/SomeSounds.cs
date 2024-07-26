// Property of Small-Ocelot https://github.com/Small-Ocelot

using System.Collections;
using UnityEngine;

public class SomeSounds : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public float minInterval = .25f;
    public float maxInterval = 2f;
    private Coroutine playSounds;


    private void OnSceneTransitionTrigger(float transitionTime, bool fadeToBlack) {
        if (fadeToBlack) {
            if (playSounds != null) StopCoroutine(playSounds);
        } else {
            playSounds = StartCoroutine(PlayRandomSound());
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound() {
        while (true) {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();

            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }


    private void OnEnable()
    {
        UITransitionController.OnGameStart += OnSceneTransitionTrigger;
    }

    private void OnDisable() {
        UITransitionController.OnGameStart -= OnSceneTransitionTrigger;
    }

}
