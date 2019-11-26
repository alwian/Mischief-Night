using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : DimensionedObject, IInteractable
{
    [SerializeField] Dimension dimension;

    public UnityAction<Collectable> OnCollect;
    public Dimension Dimension { get { return dimension; } }

    bool collected = false;

    public void Interact()
    {
        this.gameObject.SetActive(false);
        collected = true;
        OnCollect?.Invoke(this);
    }

    protected override void SetOverworld()
    {
        if (collected)
            return;
        this.gameObject.SetActive(dimension == Dimension.OVERWORLD);
    }

    protected override void SetUpsideDown()
    {
        if (collected)
            return;
        this.gameObject.SetActive(dimension == Dimension.UPSIDE_DOWN);
    }
}
