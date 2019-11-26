using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrosshairStyle
{
    Normal,
    Interaction,
    Examine,
    Grab
}

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] GameObject defaultCrosshair;
    [SerializeField] GameObject interactableCrosshair;
    [SerializeField] GameObject examinableCrosshair;
    [SerializeField] GameObject grabbableCrosshair;

    GameObject activeCrosshair;

    private void Awake()
    {
        defaultCrosshair.SetActive(false);
        interactableCrosshair.SetActive(false);
        examinableCrosshair.SetActive(false);
        grabbableCrosshair.SetActive(false);

        activeCrosshair = defaultCrosshair;
        SetCrosshairStyle(CrosshairStyle.Normal);
    }

    public void SetCrosshairStyle(CrosshairStyle style)
    {
        activeCrosshair.SetActive(false);
        
        switch (style)
        {
            case CrosshairStyle.Normal:
                activeCrosshair = defaultCrosshair;
                break;

            case CrosshairStyle.Interaction:
                activeCrosshair = interactableCrosshair;
                break;

            case CrosshairStyle.Grab:
                activeCrosshair = grabbableCrosshair;
                break;

            case CrosshairStyle.Examine:
                activeCrosshair = examinableCrosshair;
                break;

            default:
                activeCrosshair = defaultCrosshair;
                break;
        }

        activeCrosshair.SetActive(true);
    }

}
