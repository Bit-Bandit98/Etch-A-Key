using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainControllerUI : MonoBehaviour
{

    [SerializeField] private Brush PlayerBrush;
    [SerializeField] private TexturePixelSelector Selector;
    [SerializeField] private ColourSelection ColourSelector;

    [SerializeField] private TextMeshProUGUI BrushSizeText;
    [SerializeField] private TextMeshProUGUI SelectorSpeedText;
    [SerializeField] private Image ColourImage;

    [SerializeField] private string BrushSizePrefix = "Brush Size: ";
    [SerializeField] private string SelectorSpeedPrefix = "Selector Speed: ";

    public static event Action<float, bool> OnGameStart;
    private float transitionTime = 1f;

    private EtchActions Controls;
    private bool canGoBackToTitle = false;

    private void Update()
    {
        UpdateUI();
    }

    private void Start() {
        OnGameStart?.Invoke(transitionTime, false);
        Controls = new EtchActions();
        Controls.Enable();
        Controls.Player.BackToTitle.performed += ctx => GoBackToTitle();
        Invoke(nameof(EnableBackToTitle), transitionTime + 1f);
    }

    private void EnableBackToTitle() {
        canGoBackToTitle = true;
    }


    public void UpdateUI()
    {
        UpdateBrushSizeUI();
        UpdateSelectorSpeedUI();
        UpdateColourUI();
    }


    public void UpdateBrushSizeUI()
    {
        BrushSizeText.text = BrushSizePrefix + PlayerBrush.GetBrushRadius().ToString() + "px";
    }

    public void UpdateSelectorSpeedUI()
    {
        SelectorSpeedText.text = SelectorSpeedPrefix + Selector.GetSelectorSpeed() + "x";
    }

    public void UpdateColourUI()
    {
        ColourImage.color = ColourSelector.GetCurrentColour();
    }

    public void GoBackToTitle() {
        if (!canGoBackToTitle) return;
        canGoBackToTitle = false;
        OnGameStart?.Invoke(transitionTime, true);
        StartCoroutine(StartGameTransition());
    }

    private IEnumerator StartGameTransition() {
        yield return new WaitForSeconds(transitionTime + .1f);
        SceneManager.LoadScene("Title");
    }

}