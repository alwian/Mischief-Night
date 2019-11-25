using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTextureUpdate : DimensionedObject
{
    private GameObject obj;
    public Material norm;
    public Material dim;
    protected override void SetOverworld()
    {
        obj.GetComponent<MeshRenderer>().material = norm;
    }

    protected override void SetUpsideDown()
    {
        obj.GetComponent<MeshRenderer>().material = dim;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Comma))
        {
            DimensionManager.Instance.SetDimension(Dimension.UPSIDE_DOWN);
        }
        if (Input.GetKey(KeyCode.Period))
        {
            DimensionManager.Instance.SetDimension(Dimension.OVERWORLD);
        }
    }
}
