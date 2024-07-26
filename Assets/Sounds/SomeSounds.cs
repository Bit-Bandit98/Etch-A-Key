using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeSounds : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public float minInterval = .25f;
    public float maxInterval = 2f;
    private Coroutine playSounds;

    private void Awake() {
        UIControls.OnGameStart += OnSceneTransitionTrigger;
    }

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

    private void OnDisable() {
        UIControls.OnGameStart -= OnSceneTransitionTrigger;
    }

}
