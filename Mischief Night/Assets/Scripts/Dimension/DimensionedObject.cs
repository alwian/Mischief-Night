using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DimensionedObject : MonoBehaviour
{
    protected Dimension currentDimension;

    protected void Start()
    {
        DimensionManager.Instance.RegisterObject(this);
    }

    public void SetDimension(Dimension d)
    {
        if (d == currentDimension)
            return;

        currentDimension = d;

        if (currentDimension == Dimension.OVERWORLD)
            SetOverworld();
        else
            SetUpsideDown();
    }

    protected abstract void SetOverworld();
    protected abstract void SetUpsideDown();

}
