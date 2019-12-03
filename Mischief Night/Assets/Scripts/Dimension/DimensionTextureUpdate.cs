using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTextureUpdate : DimensionedObject
{
    public Material norm;
    public Material dim;

    protected void Start()
    {
        norm = gameObject.GetComponent<Renderer>().material;
    }
    protected override void SetOverworld()
    {
        gameObject.GetComponent<Renderer>().material = norm;
        GameObject lights = GameObject.Find("HighSchool Lights");
        Light[] childLights = lights.GetComponentsInChildren<Light>();
        foreach (Light l in childLights)
        {
            l.color = Color.white;
        }
    }

    protected override void SetUpsideDown()
    {
        gameObject.GetComponent<Renderer>().material = dim;

        GameObject lights = GameObject.Find("HighSchool Lights");
        Light[] childLights = lights.GetComponentsInChildren<Light>();
        foreach(Light l in childLights)
        {
            l.color = Color.red;
        }
    }
}
