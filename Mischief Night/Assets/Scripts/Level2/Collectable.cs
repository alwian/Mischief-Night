using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour, IInteractable
{
    public UnityAction<Collectable> OnCollect;

    public void Interact()
    {
        this.gameObject.SetActive(false);
        OnCollect?.Invoke(this);
    }
}
