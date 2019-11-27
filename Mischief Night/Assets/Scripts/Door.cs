/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door Options")]
    [SerializeField] Vector3 openRotation;
    [SerializeField] Vector3 closeRotation;
    [SerializeField] Transform hingeTransform;
    [SerializeField] float swingTime = 1f;

    bool doorOpen = false;

    public void Interact()
    {
        doorOpen = !doorOpen;
        StopAllCoroutines();
        StartCoroutine(SwingDoor());
    }

    private IEnumerator SwingDoor()
    {
        Quaternion startRot = hingeTransform.localRotation;

        Quaternion targetRot;
        if (doorOpen)
            targetRot = Quaternion.Euler(openRotation);
        else
            targetRot = Quaternion.Euler(closeRotation);

        float timer = 0f;
        while(timer < swingTime)
        {
            timer += Time.deltaTime;
            hingeTransform.localRotation = Quaternion.Slerp(startRot, targetRot, (timer/swingTime));
            yield return null;
        }
    }
}
