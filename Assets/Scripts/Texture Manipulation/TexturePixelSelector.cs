using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePixelSelector : MonoBehaviour
{

    [SerializeField] private TextureGeneration TextureSelected = null;
    [SerializeField] private int SelectorSpeed = 1, SelectorSpeedMin = 1, SelectorSpeedMax = 10;
    [SerializeField, Range(0,0.5f)] private float BorderOffset = 0f;

    private int CurrentXCoordinate, CurrentYCoordinate;
    private int LastXCoordinate, LastYCoordinate;

    private void Start()
    {
        if (TextureSelected == null) TextureSelected = GetComponent<TextureGeneration>();

        SetCurrentXCoordinate((int)(TextureSelected.GetWidth()) / 2);
        SetCurrentYCoordinate((int)(TextureSelected.GetHeight()) / 2);

        LastXCoordinate = CurrentXCoordinate;
        LastYCoordinate = CurrentYCoordinate;
       
    }


    public void MoveSelection(int XDistance, int YDistance)
    {
        if (XDistance == 0 && YDistance == 0) return;
        AddToCurrentXCoordinates(XDistance);
        AddToCurrentYCoordinates(YDistance);
    }


    private void SetCurrentXCoordinate(int NewXCoordinate)
    {
        LastXCoordinate = CurrentXCoordinate;

        if (NewXCoordinate < TextureSelected.GetWidth() * BorderOffset)
        {
            CurrentXCoordinate = Mathf.RoundToInt(TextureSelected.GetWidth() * BorderOffset);

        }
        else if (NewXCoordinate >= TextureSelected.GetWidth() * (1 - BorderOffset))
        {
            CurrentXCoordinate = Mathf.RoundToInt(TextureSelected.GetWidth() * (1 - BorderOffset) -1 );
        }
        else CurrentXCoordinate = NewXCoordinate;
    }

    private void SetCurrentYCoordinate(int NewYCoordinate)
    {
        LastYCoordinate = CurrentYCoordinate;

        if ( NewYCoordinate < TextureSelected.GetHeight() * BorderOffset)
        {
            CurrentYCoordinate = Mathf.RoundToInt(TextureSelected.GetHeight() * BorderOffset);

        }
        else if (NewYCoordinate >= TextureSelected.GetHeight() * (1 - BorderOffset) )
        {
            CurrentYCoordinate = Mathf.RoundToInt(TextureSelected.GetHeight() * (1 - BorderOffset) - 1);
        }
        else CurrentYCoordinate = NewYCoordinate;
    }

    private void AddToCurrentYCoordinates(int Addition)
    {
        SetCurrentYCoordinate(CurrentYCoordinate + (Addition * SelectorSpeed));
    }

    private void AddToCurrentXCoordinates(int Addition)
    {
        SetCurrentXCoordinate(CurrentXCoordinate + (Addition * SelectorSpeed));
    }


    public TextureGeneration GetTextureSelected()
    {
        return TextureSelected;
    }

    public Vector2Int GetCurrentCoordinates()
    {
        return new Vector2Int(CurrentXCoordinate, CurrentYCoordinate);
    }

    public Vector2Int GetLastCoordinates()
    {
        return new Vector2Int(LastXCoordinate, LastYCoordinate);
    }
 
    public bool GetHasMoved()
    {
        return !(LastXCoordinate == CurrentXCoordinate && LastYCoordinate == CurrentYCoordinate);
    }

    public void EqualiseLastCurrentCoordinates()
    {
        LastXCoordinate = CurrentXCoordinate;
        LastYCoordinate = CurrentYCoordinate;
    }

    public void SetSelectorSpeed(int NewSelectorSpeed)
    {
        if (NewSelectorSpeed < SelectorSpeedMin) SelectorSpeed = SelectorSpeedMin;
        else if (NewSelectorSpeed > SelectorSpeedMax) SelectorSpeed = SelectorSpeedMax;
        else SelectorSpeed = NewSelectorSpeed;
    }

    public void AddSelectorSpeed(int Addition)
    {
        SetSelectorSpeed(SelectorSpeed + Addition);
    }

    public int GetSelectorSpeed()
    {
        return SelectorSpeed;
    }
}