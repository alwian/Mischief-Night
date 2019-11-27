/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct that holds the references to a renderer and its material for each dimension
/// </summary>
[System.Serializable]
struct DimensionedRenderer
{
    public Renderer renderer;
    public Material overworldMat;
    public Material upsideDownMat;
}

/// <summary>
/// Serves as a generic way to allow many objects and renders to be 'Dimensioned' without multiple scripts
/// </summary>
public class DimensionedCollection : DimensionedObject
{
    [SerializeField] List<GameObject> overworldOnly = new List<GameObject>();
    [SerializeField] List<GameObject> upsidedownOnly = new List<GameObject>();
    [SerializeField] List<DimensionedRenderer> dimensionedRenderers = new List<DimensionedRenderer>();

    protected override void Awake()
    {
        base.Awake();
        SetOverworld();
    }

    protected override void SetOverworld()
    {
        foreach (var obj in overworldOnly)
            obj.SetActive(true);

        foreach (var obj in upsidedownOnly)
            obj.SetActive(false);

        foreach (var dr in dimensionedRenderers)
            dr.renderer.material = dr.overworldMat;
    }

    protected override void SetUpsideDown()
    {
        foreach (var obj in overworldOnly)
            obj.SetActive(false);

        foreach (var obj in upsidedownOnly)
            obj.SetActive(true);

        foreach (var dr in dimensionedRenderers)
            dr.renderer.material = dr.overworldMat;
    }
}
