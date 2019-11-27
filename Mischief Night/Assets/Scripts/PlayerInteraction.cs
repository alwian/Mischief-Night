/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Options")]
    [SerializeField] float interactionDistance;
    [SerializeField] Transform handsTransform;
    [SerializeField] LayerMask interactionMask;

    [SerializeField] ExamineGui examineGui;
    [SerializeField] CrosshairManager crosshairManager;

    PlayerController controller;
    new Camera camera;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        camera = Camera.main;
    }


    IInteractable interactable;
    IExaminable examinable;
    IPhysicsObject physObject;

    IPhysicsObject heldObject;
    bool HoldingObject { get { return !heldObject.IsNull(); } }

    void Update()
    {

        UpdateTargets();

        bool interact = Input.GetButtonDown("Interact");
        if (!interact)
            return;

        // Deal with held objects first
        if (HoldingObject)
        {
            heldObject.Drop();
            heldObject.transform.parent = null;
            heldObject = null;
            return;
        }
        else if (physObject.IsNull() == false)
        {
            heldObject = physObject;
            heldObject.Pickup(handsTransform);
            return;
        }

        // Otherwise, handle interaction and examining
        if (interactable.IsNull() == false)
        {
            interactable.Interact();
            return;
        }
        else if (examinable.IsNull() == false)
        {
            examineGui.SetExamine(examinable);
            return;
        }
    }


    private void UpdateTargets()
    {
        // No targets if control is disabled
        if (!controller.enableCameraControl)
        {
            interactable = null;
            examinable = null;
            physObject = null;
            crosshairManager.SetCrosshairStyle(CrosshairStyle.Normal);
            return;
        }

        // Try to update references for the object we are looking at
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactionDistance, interactionMask))
        {
            interactable = hit.collider.GetComponentInParent<IInteractable>();
            examinable = hit.collider.GetComponentInParent<IExaminable>();
            physObject = hit.collider.GetComponentInParent<IPhysicsObject>();

            if (interactable.IsNull() == false || physObject.IsNull() == false)
                crosshairManager.SetCrosshairStyle(CrosshairStyle.Interaction);
            else if (examinable.IsNull() == false)
                crosshairManager.SetCrosshairStyle(CrosshairStyle.Examine);
            else
                crosshairManager.SetCrosshairStyle(CrosshairStyle.Normal);
        }
        else
        {
            interactable = null;
            examinable = null;
            physObject = null;
            crosshairManager.SetCrosshairStyle(CrosshairStyle.Normal);
        }
    }
}
