using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    // Check if an interface is "null" or destroyed.
    // Casts to Monobehaviour if needed to check for destroyed objects.
    public static bool IsNull(this IGameObject obj)
    {
        // Null check
        if (obj == null)
            return true;
        // Destroyed check
        return !(obj as MonoBehaviour);
    }



    // Resets the transform's local positioning
    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
