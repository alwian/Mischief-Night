/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionPortal : DimensionedObject, IInteractable
{
    [SerializeField] GameObject visuals;

    [SerializeField] bool visibleInOverworld;
    [SerializeField] bool visibleInUpsideDown;

    public void Interact()
    {
        if (currentDimension == Dimension.OVERWORLD)
            EnterUpsideDown();
        else
            EnterOverworld();
    }

    protected override void SetOverworld()
    {
        visuals.SetActive(visibleInOverworld);
    }

    protected override void SetUpsideDown()
    {
        visuals.SetActive(visibleInUpsideDown);
    }

    private void EnterUpsideDown()
    {
        DimensionManager.Instance.SetDimension(Dimension.UPSIDE_DOWN);
    }
    private void EnterOverworld()
    {
        DimensionManager.Instance.SetDimension(Dimension.OVERWORLD);
    }
}
