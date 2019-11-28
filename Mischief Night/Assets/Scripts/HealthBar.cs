/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] RectTransform valueBar;
    [SerializeField] RectTransform totalBar;

    [Header("Options")]
    [SerializeField] float hideDelay = 3.5f;

    public void UpdateDisplay(float value)
    {
        Show();

        var size = valueBar.sizeDelta;
        size.x = totalBar.sizeDelta.x * value;

        valueBar.sizeDelta = size;

        StartCoroutine(DelayedHide());
    }

    private void Show()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(hideDelay);
        Hide();
    }
}
