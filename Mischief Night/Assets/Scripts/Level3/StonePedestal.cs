/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StonePedestal : MonoBehaviour, IInteractable
{
    [Header("Required References")]
    [SerializeField] GameObject stoneVisuals;
    [SerializeField] Node node;
    public Node Node { get { return node; } }

    public UnityAction<StonePedestal> OnPlace;
    bool stonePlaced = false;

    public void Interact()
    {
        if (stonePlaced)
            return;

        stonePlaced = true;
        stoneVisuals.SetActive(true);
        OnPlace?.Invoke(this);
    }
}
