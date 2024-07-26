using UnityEngine;

public class UIControls : MonoBehaviour
{
    private EtchActions Controls;
    [SerializeField] private UITransitionController UITransitionControl;
    private void Awake()
    {
        Initialise();
    }

    private void Initialise()
    {
        Controls = new EtchActions();
        Controls.UI.StartGame.performed += ctx => UITransitionControl.StartGamePerformed();
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