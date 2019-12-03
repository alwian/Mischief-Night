/*
 * Author: Colton Campbell (B00693513)
 * 
 * Modified by: Amanda Norman (B00850615)
 *              Alex Anderson (B00850616)  
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
    [SerializeField] public bool locked;

    private new AudioSource audio; /* Added audio */
    [SerializeField] AudioClip swingClip;

    public bool doorOpen = false;

    public void Interact()
    {
        if (!locked)
        {
            doorOpen = !doorOpen;
            StopAllCoroutines();
            StartCoroutine(SwingDoor());
        }
    }

    private IEnumerator SwingDoor()
    {
        Quaternion startRot = hingeTransform.localRotation;

        Quaternion targetRot;
        if (doorOpen)
            targetRot = Quaternion.Euler(openRotation);
        else
            targetRot = Quaternion.Euler(closeRotation);

        /* Get Game Object Audio */
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = swingClip;
        audio.Play();/* Play audio noise */

        float timer = 0f;
        while(timer < swingTime)
        {
            timer += Time.deltaTime;
            hingeTransform.localRotation = Quaternion.Slerp(startRot, targetRot, (timer/swingTime));
            yield return null;
        }
    }
}
