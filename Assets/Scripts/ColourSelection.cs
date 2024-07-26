using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ColourSelection : MonoBehaviour
{

    [SerializeField] private Color32[] Colours = null;
    private int CurrentColourIndex = 0;
    private void SetColourIndex(int NewColourIndex)
    {
        if (NewColourIndex < 0) CurrentColourIndex = Colours.Length - 1;
        else if (NewColourIndex >= Colours.Length) CurrentColourIndex = 0;
        else CurrentColourIndex = NewColourIndex;
    }

    public void NextColour()
    {
        SetColourIndex(CurrentColourIndex + 1);
    }

    public void ChangeColour(int Index, bool Addition = false)
    {
        if(!Addition) SetColourIndex(Index);
        else SetColourIndex(CurrentColourIndex + Index);

    }




    public Color32 GetCurrentColour()
    {
        return Colours[CurrentColourIndex];
    }
}
