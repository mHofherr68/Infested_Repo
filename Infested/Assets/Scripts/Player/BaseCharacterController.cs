using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    // Set externally by "PlayAudioTrigger.cs" to temporarily modify the player's movement speed.
    [HideInInspector] public float externalSpeedOffset = 0f;

    // Set externally by "PlayAudioTrigger.cs" to lock player input (e.g., during cutscenes or interactions).
    [HideInInspector] public bool inputLocked = false;

    private PlayerInput playerInput;
    private Rigidbody rb;

    // Current movement input vector (X = horizontal, Y = vertical).
    [HideInInspector] public Vector2 movementInput;

    private Transform cameraTransform;

    [Header("Player Movement Settings")]
    [Space(16)]
    // Base player movement speed.
    public float actualMovementSpeed = 8f;
    // Force applied when jumping.
    [SerializeField] private float jumpStrength = 8f;
    // Maximum vertical jump velocity.
    [SerializeField] private float maxJumpVelocity = 10f;

    [Header("UI Settings -> Inventory")]
    [Space(16)]
    // Reference to the inventory panel UI.
    [SerializeField] private GameObject selectPanel;

    [Header("Player Ground Detection")]
    [Space(16)]
    // True if the player is standing on the ground.
    public bool isGrounded;
    // Distance for the ground detection ray.
    [SerializeField] private float raycastDistance;
    // Which layers are considered as ground.
    [SerializeField] private LayerMask groundLayer;

    [Header("Collision Stop Settings")]
    [Space(16)]
    // Distance to check for walls in front of the player.
    [SerializeField] private float collisionCheckDistance = 0.5f;
    // Which layers are considered as walls.
    [SerializeField] private LayerMask wallLayer;

    [Header("Select Item At Player")]
    [Space(16)]
    // Reference to an selected item the player is using (e.g. Flashlight)
    public GameObject ItemAtPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Subscribe to input action callbacks
        playerInput.actions["Move"].performed += onMove;
        playerInput.actions["Move"].canceled += onMove;
        playerInput.actions["Jump"].performed += onJump;
        playerInput.actions["Jump"].canceled += onJump;
        playerInput.actions["Inventory"].performed += onInventory;

        cameraTransform = Camera.main.transform;
    }

    /// <summary>
    /// Handles player movement input.
    /// </summary>
    public void onMove(CallbackContext ctx)
    {
        if (inputLocked)
        {
            movementInput = Vector2.zero;
            return;
        }
        movementInput = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// Handles jump input. Adds upward force if grounded.
    /// </summary>
    public void onJump(CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (inputLocked) return;

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            // Clamp jump velocity to avoid excessive upward force
            Vector3 vel = rb.linearVelocity;
            if (vel.y > maxJumpVelocity)
            {
                vel.y = maxJumpVelocity;
                rb.linearVelocity = vel;
            }
        }
    }

    /// <summary>
    /// Handles inventory input. Toggles the inventory UI panel.
    /// </summary>
    public void onInventory(CallbackContext ctx)
    {
        if (ctx.performed && selectPanel != null)
        {
            selectPanel.SetActive(!selectPanel.activeSelf);
        }
    }

    /// <summary>
    /// Casts a ray downward to check if the player is grounded.
    /// </summary>
    private bool RaycastHitsGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

    /// <summary>
    /// Refreshes movement input manually (used when input may need to be re-polled).
    /// </summary>
    public void RefreshInput()
    {
        if (inputLocked || playerInput == null) return;
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (inputLocked) movementInput = Vector2.zero;

        // Movement is relative to camera orientation
        Vector3 movementDirection = cameraTransform.right * movementInput.x + cameraTransform.forward * movementInput.y;
        movementDirection = Vector3.ProjectOnPlane(movementDirection, Vector3.up);

        // Check for walls in movement direction
        if (movementInput.magnitude > 0.01f)
        {
            float radius = 0.3f;
            float height = 2f;
            Vector3 point1 = transform.position + Vector3.up * radius;
            Vector3 point2 = transform.position + Vector3.up * (height - radius);

            if (Physics.CapsuleCast(point1, point2, radius, movementDirection.normalized, out RaycastHit hit, collisionCheckDistance, wallLayer))
            {
                Debug.Log("Wall hit: " + hit.collider.name);
                movementDirection = Vector3.zero;
                movementInput = Vector2.zero;
            }
        }

        // Apply movement
        transform.Translate(movementDirection * Time.deltaTime * (actualMovementSpeed + externalSpeedOffset));

        // Update grounded state
        RaycastHitsGround();
    }
}
