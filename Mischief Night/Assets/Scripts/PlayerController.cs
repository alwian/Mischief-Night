using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera Options")]
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float minCameraRot = -80f;
    [SerializeField] float maxCameraRot = 80f;

    [Header("Physics Options")]
    [SerializeField] float movementForce;
    [SerializeField] Collider physicsCollider;
    [SerializeField] PhysicMaterial movingMaterial; 
    [SerializeField] PhysicMaterial stillMaterial; 

    private Player player;
    new private Rigidbody rigidbody;
    new private Camera camera;

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.camera = Camera.main;
    }

    float cameraRotation = 0f;
    private void Update()
    {
        // Input
        float xInput, yInput;
        xInput = Input.GetAxis("Mouse X");
        yInput = Input.GetAxis("Mouse Y");

        this.transform.rotation *= Quaternion.Euler(0f, xInput, 0f);

        cameraRotation = Mathf.Clamp(cameraRotation - yInput, minCameraRot, maxCameraRot);
        camera.transform.rotation = this.transform.rotation * Quaternion.Euler(cameraRotation, 0f, 0f);

        camera.transform.position = this.transform.position + (this.transform.rotation * cameraOffset);

    }

    private void FixedUpdate()
    {
        // Input
        float xInput, yInput;
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (xInput == 0f && yInput == 0f)
            physicsCollider.material = stillMaterial;
        else
            physicsCollider.material = movingMaterial;

        rigidbody.AddRelativeForce(new Vector3(xInput, 0f, yInput) * movementForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
