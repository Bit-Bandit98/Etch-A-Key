using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TextureGeneration : MonoBehaviour
{
    [SerializeField] private int Width = 128, Height = 128;
    private Texture2D GeneratedTexture;
    private NativeArray<Color32> TextureData;


    public void InitialiseTexture()
    {
        
        GeneratedTexture = new Texture2D(Width, Height, TextureFormat.RGBA32, false);
        GetComponent<Renderer>().material.mainTexture = GeneratedTexture;
        TextureData = GeneratedTexture.GetRawTextureData<Color32>();

        GeneratedTexture.Apply();
    }



    public void SetPixel(int x, int y, Color32 GivenColour)
    {
        TextureData[CoordinatesToDataIndex(x, y)] = GivenColour;
    }

    public void ApplyTexture()
    {
        GeneratedTexture.Apply();
    }

    private int CoordinatesToDataIndex(int X, int Y)
    {
        return X + (Y * GetWidth());
    }


    public Texture2D GetGeneratedTexture()
    {
        return GeneratedTexture;
    }


    public int GetWidth() { return Width; }

    public int GetHeight() { return Height; }

}