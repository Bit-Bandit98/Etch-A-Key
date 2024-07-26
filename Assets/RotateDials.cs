using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDials : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 1f;
    [SerializeField] private GameObject LeftDial, RightDial;
    [SerializeField] private TexturePixelSelector Selector;


    private void Update()
    {
        if (Selector.GetHasMoved())
        {
            Vector2Int CurrentCoords = Selector.GetCurrentCoordinates(), LastCoords = Selector.GetLastCoordinates();
            RotateDial(-(CurrentCoords.y - LastCoords.y), -(CurrentCoords.x - LastCoords.x));
        }
    }


    public void RotateDial(float LeftDialRotation, float RightDialRotation)
    {
        Debug.Log("LEFT DIAL " + LeftDialRotation + ". RIGHT DIAL " + RightDialRotation);
        LeftDial.transform.localRotation *= Quaternion.Euler(LeftDialRotation *RotationSpeed, 0, 0);
        RightDial.transform.localRotation *= Quaternion.Euler(RightDialRotation * RotationSpeed, 0, 0);
    }
}
