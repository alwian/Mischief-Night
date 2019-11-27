/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the GameObject face the main camera.
/// Optionally rotates 180 on the y axis as well (useful for guis)
/// </summary>
public class FaceCamera : MonoBehaviour
{
    [SerializeField] bool flip;
    private void Update()
    {
        this.transform.LookAt(Camera.main.transform);
        if (flip)
            this.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
    }
}
