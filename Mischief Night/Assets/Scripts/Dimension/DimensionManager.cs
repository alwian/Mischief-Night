﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dimension
{
    OVERWORLD,
    UPSIDE_DOWN
}

/// <summary>
/// Manages the transitions between the game's two dimensions for all objects
/// </summary>
public class DimensionManager : MonoBehaviour
{
    public static DimensionManager Instance;
    
    [SerializeField] Color overworldAmbient;
    [SerializeField] Color netherAmbient;

    public Dimension CurrentDimension { get { return currentDimension; } }
    static Dimension currentDimension;
    static List<DimensionedObject> dimensionObjects = new List<DimensionedObject>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Debug.Log("Deleting duplicate DimensionManager");
            Destroy(this);
            return;
        }
        currentDimension = Dimension.OVERWORLD;
        UpdateSky();
    }

    public void RegisterObject(DimensionedObject obj)
    {
        if (obj == null)
            return;
        dimensionObjects.Add(obj);
    }

    public void SetDimension(Dimension newDimension)
    {
        if (newDimension == currentDimension)
            return;

        currentDimension = newDimension;

        StopAllCoroutines();
        StartCoroutine(DimensionTransition());
    }

    private IEnumerator DimensionTransition()
    {
        float fadeTime = 1f;

        CameraManager.Instance.FadeOut(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        foreach(var obj in dimensionObjects)
        {
            obj.SetDimension(currentDimension);
        }

        UpdateSky();

        CameraManager.Instance.FadeIn(fadeTime);
    }

    private void UpdateSky()
    {
        if (currentDimension == Dimension.OVERWORLD)
            RenderSettings.ambientLight = overworldAmbient;
        else
            RenderSettings.ambientLight = netherAmbient;
    }

}