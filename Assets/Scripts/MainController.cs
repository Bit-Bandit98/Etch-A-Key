
using UnityEngine;

public class MainController : MonoBehaviour
{

    [SerializeField] private TextureGeneration NewTexture = null;
    [SerializeField] private ColourSelection Colours = null;
    [SerializeField] private Brush UserBrush = null;
    [SerializeField] private TexturePixelSelector Selector = null;
   // [SerializeField] private bool DisableControls = false;

    private void Awake()
    {
        NewTexture = GetComponent<TextureGeneration>();
        Colours = GetComponent<ColourSelection>();
        UserBrush = GetComponent<Brush>();
        Selector = GetComponent<TexturePixelSelector>();
    }

    private void Start()
    {
        NewTexture.InitialiseTexture();
  
    }

   

    void Update()
    {
       // if(!DisableControls) PlayerInput();

        if (Selector.GetHasMoved()) {
            DrawLine(Selector, NewTexture, Colours.GetCurrentColour());
            Selector.EqualiseLastCurrentCoordinates();

        }

    }

    Vector2Int NewMovementCache = new Vector2Int(0,0);
    private void PlayerInput()
    {
        NewMovementCache = Vector2Int.zero;

        if (Input.GetKey(KeyCode.A)) NewMovementCache += Vector2Int.left;
        if (Input.GetKey(KeyCode.D)) NewMovementCache += Vector2Int.right;
        if (Input.GetKey(KeyCode.W)) NewMovementCache += Vector2Int.up;
        if (Input.GetKey(KeyCode.S)) NewMovementCache += Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.Space)) NewTexture.InitialiseTexture();
        if (Input.GetKeyDown(KeyCode.R)) UserBrush.SetRounded(!UserBrush.GetRounded());


        Selector.MoveSelection(NewMovementCache.x, NewMovementCache.y);


    }

    private int x0, x1, y0, y1;
    private void DrawLine(TexturePixelSelector Points, TextureGeneration texture, Color32 color)
    {
        x0 = Points.GetLastCoordinates().x;
        x1 = Points.GetCurrentCoordinates().x;
        y0 = Points.GetLastCoordinates().y;
        y1 = Points.GetCurrentCoordinates().y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            // Paint the pixel at the current position
            UserBrush.PaintPixels(texture, x0, y0, color);

            // Check if we have reached the end point
            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }


    }

}
