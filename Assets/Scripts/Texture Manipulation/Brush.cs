using UnityEngine;

public class Brush : MonoBehaviour
{

    [SerializeField] private int BrushSize = 1;
    [SerializeField] private int BrushSizeMin = 1, BrushSizeMax = 100;
    [SerializeField] private bool Rounded = true;


    public void PaintPixels(TextureGeneration GivenTG, int XCoordinate, int YCoordinate, Color32 PixelColor)
    {
        //Setting bounds to prevent "Index out of bounds" errors
        int minX = Mathf.Max(0, XCoordinate - BrushSize);
        int maxX = Mathf.Min(GivenTG.GetWidth() -1 , XCoordinate + BrushSize);
        int minY = Mathf.Max(0, YCoordinate - BrushSize);
        int maxY = Mathf.Min(GivenTG.GetHeight()  -1 , YCoordinate + BrushSize);

        
        for (int i = minX; i <= maxX; i++)
        {
            for (int ii = minY; ii <= maxY; ii++)
            {
                if (!Rounded)
                {
                    GivenTG.SetPixel(i, ii, PixelColor);

                }
                else if (Vector2.Distance(new Vector2(i, ii), new Vector2(XCoordinate, YCoordinate)) <= BrushSize)
                {
                    GivenTG.SetPixel(i, ii, PixelColor);
                }

            }

        }

        GivenTG.ApplyTexture();

    }


    public void SetBrushRadius(int BrushRadius)
    {
        if (BrushRadius < BrushSizeMin) BrushSize = BrushSizeMin;
        else if (BrushRadius > BrushSizeMax) BrushSize = BrushSizeMax;
        else BrushSize = BrushRadius;
    }

    public void AddBrushRadius(int Addition)
    {
        SetBrushRadius(BrushSize + Addition);
    }
    public int GetBrushRadius() { return BrushSize; }
    public void SetRounded(bool SetRounded) { Rounded = SetRounded;}
    public bool GetRounded() { return Rounded; }


}