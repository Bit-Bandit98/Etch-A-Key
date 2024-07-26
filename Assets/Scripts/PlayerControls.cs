using UnityEngine;


public class PlayerControls : MonoBehaviour
{

    private EtchActions Controls;

    [SerializeField] private TexturePixelSelector Selector;
    [SerializeField] private TextureGeneration SelectedTexture;
    [SerializeField] private Brush CurrentBrush;
    [SerializeField] private ColourSelection SelectedColour;
    [SerializeField] private MainControllerUI UIController;
    [SerializeField] private UITransitionController UITransitionControl;

    private void Awake()
    {
        Initialise();
    }

    private void Initialise()
    {
        Controls = new EtchActions();
        Controls.Player.CleanCanvas.performed += ctx => SelectedTexture.InitialiseTexture();
        Controls.Player.IncreaseBrushSize.performed += ctx => AlterBrushSize();
        Controls.Player.ChangeColour.performed += ctx => ChangeColour();
        Controls.Player.ChangeRoundness.performed += ctx => ChangeRoundness();
        Controls.Player.IncreaseSelectorSpeed.performed += ctx => ChangeSelectorSpeed();
        Controls.Player.BackToTitle.performed += ctx => UITransitionControl.GoBackToTitle();

    }


    private void ChangeSelectorSpeed()
    {
        Selector.AddSelectorSpeed(Mathf.RoundToInt(Controls.Player.IncreaseSelectorSpeed.ReadValue<float>()));
    }

    private void ChangeRoundness()
    {
        CurrentBrush.SetRounded(!CurrentBrush.GetRounded());
    }

    private void ChangeColour()
    {
        SelectedColour.ChangeColour(Mathf.RoundToInt(Controls.Player.ChangeColour.ReadValue<float>()), true);
    }

    private void AlterBrushSize()
    {
        CurrentBrush.AddBrushRadius(Mathf.RoundToInt(Controls.Player.IncreaseBrushSize.ReadValue<float>()));
    }

    private void Update()
    {
        if (Controls.Player.Movement.IsPressed())
        {
            Movement();

        }
    }

    private void Movement()
    {
        Vector2Int NewValue = new Vector2Int(Mathf.RoundToInt(Controls.Player.Movement.ReadValue<Vector2>().x), Mathf.RoundToInt(Controls.Player.Movement.ReadValue<Vector2>().y));
        Selector.MoveSelection(NewValue.x, NewValue.y);
        
    }
    private void OnEnable()
    {
        Controls.Enable();
    }


    private void OnDisable()
    {
        Controls.Disable();
    }
}