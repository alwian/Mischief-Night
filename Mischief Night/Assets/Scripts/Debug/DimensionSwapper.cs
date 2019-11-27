/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwapper : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Swap();
    }


    private void Swap()
    {
        Dimension dim = DimensionManager.Instance.CurrentDimension;

        if (dim == Dimension.OVERWORLD)
            DimensionManager.Instance.SetDimension(Dimension.UPSIDE_DOWN);
        else
            DimensionManager.Instance.SetDimension(Dimension.OVERWORLD);
    }
}
