/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminableObject : MonoBehaviour, IExaminable
{
    [SerializeField] string examineText;

    public string GetExamine()
    {
        return examineText;
    }
}
