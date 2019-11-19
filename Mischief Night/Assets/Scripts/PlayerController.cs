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
    [SerializeField] float movementForce = 100f;
    [SerializeField] float maxMovementSpeed = 5f;
    [SerializeField] Collider physicsCollider;
    [SerializeField] PhysicMaterial movingMaterial; 
    [SerializeField] PhysicMaterial stillMaterial;

    [Header("Enable/Disable")]
    public bool enablePlayerControl = true;
    public bool enableCameraControl = true;


    private Player player;
    new private Rigidbody rigidbody;
    new private Camera camera;

    public void SetMaxMovementSpeed(float newMax)
    {
        maxMovementSpeed = Mathf.Clamp(newMax, 0f, 100f);
    }

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.camera = Camera.main;
    }

    float cameraRotation = 0f;
    private void Update()
    {
        if (!enableCameraControl)
            return;

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
        if (!enablePlayerControl)
            return;

        // Input
        Vector3 input = new Vector3 (
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );

        if (input.sqrMagnitude > 1f)
            input = input.normalized;

        if (input.sqrMagnitude == 0f)
            physicsCollider.material = stillMaterial;
        else
            physicsCollider.material = movingMaterial;

        rigidbody.AddRelativeForce(input * movementForce * Time.fixedDeltaTime, ForceMode.Impulse);

        // Get the velocity and take out the gravity
        var vel = rigidbody.velocity;
        var grav = vel.y;
        vel.y = 0;

        // Clamp the gravity-less velocity and reapply gravity
        vel = Vector3.ClampMagnitude(vel, maxMovementSpeed);
        vel.y = grav;

        rigidbody.velocity = vel;
    }
}
