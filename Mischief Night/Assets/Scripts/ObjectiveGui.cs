/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveGui : MonoBehaviour
{
    [SerializeField] Text objectiveText;

    public void SetObjective(string objective)
    {
        objectiveText.text = "- " + objective;
    }
}
