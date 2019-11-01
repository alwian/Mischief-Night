using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example of a dimensioned object that changes with the current dimension
/// </summary>
public class DimensionalObj : DimensionedObject
{
    [SerializeField] Renderer graphics;
    [SerializeField] GameObject boo;
    [SerializeField] Color overworldColor = Color.white;
    [SerializeField] Color netherColor = Color.white;

    private void Awake()
    {
        SetOverworld();
    }

    protected override void SetOverworld()
    {
        graphics.material.color = overworldColor;
        boo.SetActive(false);
    }

    protected override void SetUpsideDown()
    {
        graphics.material.color = netherColor;
        boo.SetActive(true);
    }
}
