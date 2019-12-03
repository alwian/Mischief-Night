/*
 * Author: Colton Campbell (B00693513)
 */
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip stepSound;

    [Header("Camera Options")]
    [SerializeField] float sensitivity = 1f;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float minCameraRot = -80f;
    [SerializeField] float maxCameraRot = 80f;

    [Header("Movement Options")]
    [SerializeField] float movementForce = 100f;
    [SerializeField] float maxMovementSpeed = 5f;
    [SerializeField] float sprintMultiplier = 1.5f;

    [Header("Movement Flow")]
    [SerializeField] float maxFlowMultiplier = 1.35f;
    [SerializeField] float flowStartTime = 3.5f;
    [SerializeField] float flowRate = 0.5f;
    [SerializeField] float maxFlowTurnAngle = 65f;
    [SerializeField] float flowThreshold = 0.95f;
    [SerializeField] float cameraShakeSpeed = 5f;
    [SerializeField] float cameraShakeStrength = 0.1f;

    [Header("Physics Options")]
    [SerializeField] Collider physicsCollider;
    [SerializeField] PhysicMaterial movingMaterial; 
    [SerializeField] PhysicMaterial stillMaterial;

    [Header("Enable/Disable")]
    public bool enablePlayerControl = true;
    public bool enableCameraControl = true;


    private Player player;
    new private Rigidbody rigidbody;
    new private Camera camera;

    // Enables physics on the camera, making it fall to the floor
    bool cameraDropped = false;
    public void DropCamera()
    {
        cameraDropped = true;

        var R = camera.GetComponent<Rigidbody>();
        R.isKinematic = false;
    }

    public void PickupCamera()
    {
        cameraDropped = false;

        var R = camera.GetComponent<Rigidbody>();
        R.isKinematic = true;
    }

    private void Awake()
    {
        this.player = GetComponent<Player>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.camera = Camera.main;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("WalkSound", 0f, 0.5f);
    }

    private void WalkSound()
    {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            audioSource.PlayOneShot(stepSound);
        } 
    }

    float cameraRotation = 0f;
    float cameraShakeTimer = 0f;
    private void Update()
    {
        if (!enableCameraControl)
            return;

        // Input
        float xInput, yInput;
        xInput = Input.GetAxis("Mouse X") * sensitivity;
        yInput = Input.GetAxis("Mouse Y") * sensitivity;

        this.transform.rotation *= Quaternion.Euler(0f, xInput, 0f);

        cameraRotation = Mathf.Clamp(cameraRotation - yInput, minCameraRot, maxCameraRot);
        camera.transform.rotation = this.transform.rotation * Quaternion.Euler(cameraRotation, 0f, 0f);

        if (!cameraDropped)
        {
            camera.transform.position = this.transform.position + (this.transform.rotation * cameraOffset);
            if (flowActive)
            {
                cameraShakeTimer += Time.deltaTime * cameraShakeSpeed * activeFlowMult * Random.Range(0.8f, 1.2f);
                camera.transform.position += (Vector3.up * cameraShakeStrength) * Mathf.Sin(cameraShakeTimer);
            }
            else
                cameraShakeTimer = 0f;
        }
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

        bool isSprinting = Input.GetButton("Sprint");

        if (input.sqrMagnitude > 1f)
            input = input.normalized;

        if (input.sqrMagnitude == 0f)
            physicsCollider.material = stillMaterial;
        else
            physicsCollider.material = movingMaterial;

        if (isSprinting)
            input *= sprintMultiplier;

        rigidbody.AddRelativeForce(input * movementForce * Time.fixedDeltaTime, ForceMode.Impulse);

        // Get the velocity and take out the gravity
        var vel = rigidbody.velocity;
        var grav = vel.y;
        vel.y = 0;

        // Adjust max speed
        var activeMaxSpeed = maxMovementSpeed * activeFlowMult;
        if (isSprinting)
            activeMaxSpeed *= sprintMultiplier;

        // Clamp the gravity-less velocity and reapply gravity
        vel = Vector3.ClampMagnitude(vel, activeMaxSpeed);

        // Adjust flow (movement speed bonus)
        //
        var worldSpaceInput = this.transform.rotation * input;
        // Going fast enough and not turning to sharply
        if (vel.magnitude / (maxMovementSpeed*sprintMultiplier) > flowThreshold && Vector3.Angle(vel, worldSpaceInput) < maxFlowTurnAngle)
            IncreaseFlow();
        else
            ResetFlow();


        vel.y = grav;

        rigidbody.velocity = vel;
    }

    bool flowActive;
    public float activeFlowMult = 1f;
    float flowTimer = 0f;
    private void IncreaseFlow()
    {
        // Set the timer when first enabling
        if (!flowActive)
            flowTimer = Time.time + flowStartTime;

        // Increase flow once the timer is over
        if (Time.time > flowTimer)
            activeFlowMult = Mathf.Clamp(activeFlowMult + (flowRate * Time.fixedDeltaTime), 1f, maxFlowMultiplier);

        flowActive = true;
    }

    // Reset flow
    private void ResetFlow()
    {
        flowActive = false;
        activeFlowMult = 1f;
    }
}
