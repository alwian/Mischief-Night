using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamineGui : MonoBehaviour
{
    [SerializeField] Text examineText;
    [SerializeField] float displayTime = 5f;

    public void SetExamine(IExaminable examinable)
    {
        if (examinable.IsNull())
        {
            Clear(0);
            return;
        }

        examineText.text = examinable.GetExamine();
        StopAllCoroutines();
        StartCoroutine(Clear(displayTime));
    }

    IEnumerator Clear(float delay)
    {
        yield return new WaitForSeconds(delay);
        examineText.text = "";
    }
}
