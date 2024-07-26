using UnityEngine;

public class BrushUISelection : MonoBehaviour
{
    [SerializeField] private bool Enabled = true;
    [SerializeField] private Brush Brush;
    [SerializeField] private TexturePixelSelector Selector;
    [SerializeField] private ColourSelection SelectedColour;
    [SerializeField] private GameObject Circle, Square;
    [SerializeField] private Vector3 PositionalOffset = Vector3.zero;
    [SerializeField] private bool EnableInvertedColor = false;
    private Renderer SquareRenderer, CircleRenderer, CurrentRenderer;


    private void Awake()
    {
       if(SquareRenderer == null) SquareRenderer = Square.GetComponent<Renderer>();
       if(CircleRenderer == null) CircleRenderer = Circle.GetComponent<Renderer>();
        CurrentRenderer = CircleRenderer;
    }

    private void Start()
    {
        SetSelectionShape();

    }

    private void Update()
    {
        if (Enabled) DrawBrushUI(Selector);
    }

    private GameObject SelectionShape = null;
    private void DrawBrushUI(TexturePixelSelector Selector)
    {
        SetSelectionShape();

        Vector3 CurrentPos = Selector.GetTextureSelected().gameObject.transform.position;
        float XScale = Selector.GetTextureSelected().transform.localScale.x / Selector.GetTextureSelected().GetWidth();
        float YScale = Selector.GetTextureSelected().transform.localScale.y / Selector.GetTextureSelected().GetHeight();
        SelectionShape.transform.localScale = new Vector3(XScale, YScale, 1) * Brush.GetBrushRadius() * 2;
        SelectionShape.transform.position = new Vector3(Selector.GetCurrentCoordinates().x * XScale - (Selector.GetTextureSelected().transform.localScale.x/2), Selector.GetCurrentCoordinates().y * YScale - (Selector.GetTextureSelected().transform.localScale.y / 2), 0) + CurrentPos + PositionalOffset;
        SetNegativeColour(SelectedColour.GetCurrentColour());
    }

    private void SetSelectionShape()
    {
        if(SelectionShape == null)
        {
            SelectionShape = Circle;
        }
        SelectionShape.SetActive(false);

        if (Brush.GetRounded())
        {
            
            SelectionShape = Circle;
            CurrentRenderer = CircleRenderer;
        }
        else
        {
            SelectionShape = Square;
            CurrentRenderer = SquareRenderer;
        }

        SelectionShape.SetActive(true);
    }

    private void SetNegativeColour(Color32 CurrentColour)
    {
        if (!EnableInvertedColor) return;
        Color NewColor = new Color(1- CurrentColour.r, 1 - CurrentColour.g, 1 - CurrentColour.b, 1);
        CurrentRenderer.material.SetColor("_FillColor", NewColor);
       
    }

}