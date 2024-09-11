using UnityEngine;

public class MainController : MonoBehaviour
{

    [SerializeField] private TextureGeneration NewTexture = null;
    [SerializeField] private ColourSelection Colours = null;
    [SerializeField] private Brush UserBrush = null;
    [SerializeField] private TexturePixelSelector Selector = null;

    private void Awake()
    {
       if (NewTexture == null) NewTexture = GetComponent<TextureGeneration>();
       if (Colours == null) Colours = GetComponent<ColourSelection>();
       if (UserBrush == null) UserBrush = GetComponent<Brush>();
       if (Selector == null) Selector = GetComponent<TexturePixelSelector>();
    }

    private void Start()
    {
        NewTexture.InitialiseTexture();
  
    }

    void Update()
    {
        if (Selector.GetHasMoved()) {
            DrawLine(Selector, NewTexture, Colours.GetCurrentColour());
            Selector.EqualiseLastCurrentCoordinates();
        }

    }

    //The below method should be in the brush class. It doesn't make sense to leave it here. 
    private int StartXPos, EndXPos, StartYPos, EndYPos;
    private void DrawLine(TexturePixelSelector Points, TextureGeneration texture, Color32 color)
    {

        //Bresenham's line algorithm

        StartXPos = Points.GetLastCoordinates().x;
        EndXPos = Points.GetCurrentCoordinates().x;
        StartYPos = Points.GetLastCoordinates().y;
        EndYPos = Points.GetCurrentCoordinates().y;

        int ChangeInX = Mathf.Abs(EndXPos - StartXPos);
        int ChangeInY = Mathf.Abs(EndYPos - StartYPos);
        int SignOfX = StartXPos < EndXPos ? 1 : -1;
        int SignOfY = StartYPos < EndYPos ? 1 : -1;
        int ErrorValue = ChangeInX - ChangeInY;

        while (true)
        {
            // Paint the pixel at the current position
            UserBrush.PaintPixels(texture, StartXPos, StartYPos, color);

            // Have we reached the end point?
            if (StartXPos == EndXPos && StartYPos == EndYPos) break;

            int ErrorValue2 = 2 * ErrorValue;
            if (ErrorValue2 > -ChangeInY)
            {
                ErrorValue -= ChangeInY;
                StartXPos += SignOfX;
            }
            if (ErrorValue2 < ChangeInX)
            {
                ErrorValue += ChangeInX;
                StartYPos += SignOfY;
            }
        }


    }

}