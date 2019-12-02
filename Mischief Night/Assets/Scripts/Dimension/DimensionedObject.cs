/*
 * Author: Colton Campbell (B00693513)
 */
using UnityEngine;

public abstract class DimensionedObject : MonoBehaviour
{
    protected Dimension currentDimension;

    protected virtual void Awake()
    {
        if (!DimensionManager.Instance)
            Debug.LogErrorFormat("DimensionedObjects require an instance of the DimensionManager to be in the scene.");
        else
            DimensionManager.Instance.RegisterObject(this);
    }

    public void SetDimension(Dimension d)
    {
        currentDimension = d;

        if (currentDimension == Dimension.OVERWORLD)
            SetOverworld();
        else
            SetUpsideDown();
    }

    protected abstract void SetOverworld();
    protected abstract void SetUpsideDown();

}
