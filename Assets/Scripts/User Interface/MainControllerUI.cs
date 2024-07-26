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


    private void Update()
    {
        UpdateUI();
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



}