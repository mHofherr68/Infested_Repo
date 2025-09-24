/*using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{

    // Will be set from "PlayAudioTrigger.cs"
    [HideInInspector] public float externalSpeedOffset = 0f;
//---
    [HideInInspector] public bool inputLocked = false;
//---
    // Reference to the PlayerInput component
    private PlayerInput playerInput;         

    // Reference to the Rigidbody component
    private Rigidbody rb;                    

    // Stores input values for horizontal and vertical moveme
    [HideInInspector] public Vector2 movementInput;           

    // Reference to the main camera's transform
    private Transform cameraTransform;

    // Reference to an selected item the player is using (e.g. Flashlight, Weapon)
    // public GameObject ItemAtPlayer;          

    // Spotlights GameObject for Helmet, toggle on/off
    // public GameObject SpotLights;

    [Header("Player Movement Settings")]

    [Space(16)]

    // Ability to set the player Movement Speed
    [SerializeField] public float actualMovementSpeed = 8f;

    // Ability to set the player Force, applied when jumping
    [SerializeField] private float jumpStrength = 8f;

    // Ability to set the "Jump" velocity
    [SerializeField] private float maxJumpVelocity = 10f;

    [Header("UI Settings -> inventory")]

    [Space(16)]

    // Reference to the UI inventory panel
    [SerializeField] private GameObject selectPanel;

    [Header("Player Ground Detection")]

    [Space(16)]

    // Flag to check, if the player is currently grounded
    public bool isGrounded;

    // Distance for the raycast to check for ground contact
    [SerializeField] private float raycastDistance;

    // Layer mask to identify ground objects
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        // Get references for Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Get references for required components
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Bind input actions to corresponding methods
        playerInput.actions["Move"].performed += onMove;
        playerInput.actions["Move"].canceled += onMove;
        playerInput.actions["Jump"].performed += onJump;
        playerInput.actions["Jump"].canceled += onJump;
        //playerInput.actions["Spotlight"].performed += onSpotlight;
        //playerInput.actions["Focus"].performed += onFocus;
        playerInput.actions["Inventory"].performed += onInventory;

        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    // Used when "THE PLAYER" pressed -> (WASD Keys)

//---
    public void onMove(CallbackContext ctx)
    {
        if (inputLocked)
        {
            movementInput = Vector2.zero; // Eingaben blockieren
            return;
        }

        movementInput = ctx.ReadValue<Vector2>();
    }


    /*public void onMove(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

//---

    // Used when the player presses the jump button -> (Spacebar)
    public void onJump(CallbackContext ctx)
    {
        // When the player is grounded based on the return value of RaycastHitsGround() method...
        if (isGrounded == true) 
        {
            // ...allow jumping while Player is grounded
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            // Limit vertical velocity to maxJumpVelocity
            Vector3 vel = rb.linearVelocity;

            if (vel.y > maxJumpVelocity)
            {
                vel.y = maxJumpVelocity;
                rb.linearVelocity = vel;
            }
        }
    }

    /* Used when "THE PLAYER" presses the spotlight toggle button -> (F)
    public void onSpotlight(CallbackContext ctx)
    {

        // Toggles Helmet´s Spotlight on/off
        if (ItemAtPlayer.activeSelf)
        {
            SpotLight.SetActive(!SpotLight.activeSelf);
        }
    }*/

// -> Optional FOV zoom
/*public void onFocus(CallbackContext ctx)
{
    //Debug.Log("RMouse");
    //RealizeIt !!
} 

//Used when "THE PLAYER" presses the inventory Key -> (I)
public void onInventory(CallbackContext ctx)
{

    // Check if the input action was just performed (e.g., key was pressed)
    if (ctx.performed)
    {

        // Make sure the inventory panel reference is assigned
        if (selectPanel != null)
        {

            // Check whether the inventory panel is currently active
            bool isActive = selectPanel.activeSelf;

            // Toggles the inventory UI Panel
            selectPanel.SetActive(!isActive);
        }
    }
}

// Raycast method to check if the player is grounded
private bool RaycastHitsGround() 
{
    // Perform a raycast downwards from the player's position
    RaycastHit hit;
    // Starting point of the raycast (player's position)
    Vector3 rayStart = transform.position;
    // Visualize the raycast in the Scene view for debugging
    Debug.DrawRay(rayStart, Vector3.down * raycastDistance, Color.red); 

    // Check if the player is grounded using a raycast
    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer)) {
        // If the raycast hits the ground layer within the specified distance, the player is grounded
        isGrounded = true;
    } else {
        // If the raycast does not hit anything, the player is not grounded
        isGrounded = false;
    }
    // Return the grounded status
    return isGrounded;
    }

public void FixedUpdate()
{
    // Calculate movement direction relative to the camera
    var movementDirection = cameraTransform.right * movementInput.x + cameraTransform.forward * movementInput.y;

    // Ensure movement is restricted to the horizontal plane
    movementDirection = Vector3.ProjectOnPlane (movementDirection, Vector3.up).normalized;
//---
    // Move´s the player based on direction, time, and movement speed + externalSpeedOffset
    transform.Translate(movementDirection * Time.deltaTime * (actualMovementSpeed + externalSpeedOffset));
    Debug.Log(+actualMovementSpeed + externalSpeedOffset);
//---        
    // Call the raycast method to update grounded status
    RaycastHitsGround();
}
}

using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    // Will be set from "PlayAudioTrigger.cs"
    [HideInInspector] public float externalSpeedOffset = 0f;

    // NEW: Flag um Eingaben komplett zu sperren (wird von PlayAudioOnTrigger gesetzt)
    [HideInInspector] public bool inputLocked = false; // NEW

    // Reference to the PlayerInput component
    private PlayerInput playerInput;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    // Stores input values for horizontal and vertical moveme
    [HideInInspector] public Vector2 movementInput;

    // Reference to the main camera's transform
    private Transform cameraTransform;

    [Header("Player Movement Settings")]
    [Space(16)]
    [SerializeField] public float actualMovementSpeed = 8f;
    [SerializeField] private float jumpStrength = 8f;
    [SerializeField] private float maxJumpVelocity = 10f;

    [Header("UI Settings -> inventory")]
    [Space(16)]
    [SerializeField] private GameObject selectPanel;

    [Header("Player Ground Detection")]
    [Space(16)]
    public bool isGrounded;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Bind input actions
        playerInput.actions["Move"].performed += onMove;
        playerInput.actions["Move"].canceled += onMove;
        playerInput.actions["Jump"].performed += onJump;
        playerInput.actions["Jump"].canceled += onJump;
        playerInput.actions["Inventory"].performed += onInventory;

        cameraTransform = Camera.main.transform;
    }

    // Used when "THE PLAYER" pressed -> (WASD Keys)
    public void onMove(CallbackContext ctx)
    {
        // If input is locked, null out any input (but note: this callback only runs when an input event happens)
        if (inputLocked)
        {
            movementInput = Vector2.zero;
            return;
        }

        movementInput = ctx.ReadValue<Vector2>();
    }

    // Used when the player presses the jump button -> (Spacebar)
    public void onJump(CallbackContext ctx)
    {
        // Only act on performed (avoid running on canceled)
        if (!ctx.performed) return;

        // Block jump when inputs are locked
        if (inputLocked) return;

        if (isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            // Limit vertical velocity to maxJumpVelocity
            Vector3 vel = rb.linearVelocity;

            if (vel.y > maxJumpVelocity)
            {
                vel.y = maxJumpVelocity;
                rb.linearVelocity = vel;
            }
        }
    }

    public void RefreshInput()
    {
        if (inputLocked) return;
        if (playerInput == null) return;

        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public void onInventory(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (selectPanel != null)
            {
                bool isActive = selectPanel.activeSelf;
                selectPanel.SetActive(!isActive);
            }
        }
    }*/


using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    // Will be set by "PlayAudioTrigger.cs"
    [HideInInspector] public float externalSpeedOffset = 0f;
    // Will be set by "PlayAudioTrigger.cs"
    [HideInInspector] public bool inputLocked = false;

    private PlayerInput playerInput;
    private Rigidbody rb;
    [HideInInspector] public Vector2 movementInput;
    private Transform cameraTransform;

    [Header("Player Movement Settings")]
    [SerializeField] public float actualMovementSpeed = 8f;
    [SerializeField] private float jumpStrength = 8f;
    [SerializeField] private float maxJumpVelocity = 10f;

    [Header("UI Settings -> inventory")]
    [SerializeField] private GameObject selectPanel;

    [Header("Player Ground Detection")]
    public bool isGrounded;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Collision Stop Settings")]
    [SerializeField] private float collisionCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer; // Layer deiner Wände

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        playerInput.actions["Move"].performed += onMove;
        playerInput.actions["Move"].canceled += onMove;
        playerInput.actions["Jump"].performed += onJump;
        playerInput.actions["Jump"].canceled += onJump;
        playerInput.actions["Inventory"].performed += onInventory;

        cameraTransform = Camera.main.transform;
    }

    public void onMove(CallbackContext ctx)
    {
        if (inputLocked)
        {
            movementInput = Vector2.zero;
            return;
        }

        movementInput = ctx.ReadValue<Vector2>();
    }

    public void onJump(CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (inputLocked) return;

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            Vector3 vel = rb.linearVelocity;
            if (vel.y > maxJumpVelocity)
            {
                vel.y = maxJumpVelocity;
                rb.linearVelocity = vel;
            }
        }
    }

    public void onInventory(CallbackContext ctx)
    {
        if (ctx.performed && selectPanel != null)
        {
            selectPanel.SetActive(!selectPanel.activeSelf);
        }
    }

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

    public void RefreshInput()
    {
        if (inputLocked || playerInput == null) return;
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        // Input Lock berücksichtigen
        if (inputLocked) movementInput = Vector2.zero;

        // Bewegungsrichtung
        Vector3 movementDirection = cameraTransform.right * movementInput.x + cameraTransform.forward * movementInput.y;
        movementDirection = Vector3.ProjectOnPlane(movementDirection, Vector3.up);

        // --- Wand-Kollision prüfen (CapsuleCast)
        if (movementInput.magnitude > 0.01f)
        {
            float radius = 0.3f;  // anpassen an Player Collider
            float height = 2f;    // anpassen an Player Collider
            Vector3 point1 = transform.position + Vector3.up * radius;
            Vector3 point2 = transform.position + Vector3.up * (height - radius);

            if (Physics.CapsuleCast(point1, point2, radius, movementDirection.normalized, out RaycastHit hit, collisionCheckDistance, wallLayer))
            {
                Debug.Log("Wall hit: " + hit.collider.name);
                movementDirection = Vector3.zero;   // Player stoppt
                movementInput = Vector2.zero;       // HeadBob stoppt
            }
        }

        // --- Move
        transform.Translate(movementDirection * Time.deltaTime * (actualMovementSpeed + externalSpeedOffset));

        // Grounded Status prüfen
        RaycastHitsGround();
    }
}


