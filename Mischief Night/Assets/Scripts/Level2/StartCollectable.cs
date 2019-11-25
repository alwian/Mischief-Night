using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollectable : MonoBehaviour, IInteractable
{
    [Header("Required References")]
    [SerializeField] Level2Manager manager;

    public void Interact()
    {
        manager.ActivateCollectables();
        this.gameObject.SetActive(false);
    }
}
