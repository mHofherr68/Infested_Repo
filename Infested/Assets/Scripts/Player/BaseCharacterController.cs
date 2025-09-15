using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    
    // Reference to the PlayerInput component
    private PlayerInput playerInput;         

    // Reference to the Rigidbody component
    private Rigidbody rb;                    

    // Stores input values for horizontal and vertical movement
    public Vector2 movementInput;           

    // Reference to the main camera's transform
    private Transform cameraTransform;

    // Reference to an selected item the player is using (e.g. Flashlight)
    // public GameObject ItemAtPlayer;          

    // Spotlight GameObject for Flashlight, to toggle on/off
    // public GameObject SpotLight;

    [Header("Player Movement Settings")]

    [Space(16)]

    // Ability to set the player Movement Speed
    [SerializeField] public float actualMovementSpeed;

    // Ability to set the player Force, applied when jumping
    [SerializeField] private float jumpStrength;        

    // Reference to the UI inventory panel
    //[SerializeField] private GameObject inventoryPanel; 

    // Flag to check, if the player is currently grounded
    public bool isGrounded;                 

    void Start()
    {

        // Get references to required components
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Bind input actions to corresponding methods
        playerInput.actions["Move"].performed += onMove;
        playerInput.actions["Move"].canceled += onMove;
        playerInput.actions["Jump"].performed += onJump;
        playerInput.actions["Jump"].canceled += onJump;
        //playerInput.actions["Spotlight"].performed += onSpotlight;
        //playerInput.actions["Focus"].performed += onFocus;
        //playerInput.actions["Inventory"].performed += onInventory;

        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    // Called when "THE PLAYER" pressed (WASD Keys)
    public void onMove(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        //Debug.Log("Movement Input: " + movementInput);
    }

    // Called when the player presses the jump button (Spacebarr)
    public void onJump(CallbackContext ctx)
    {
        
        // Jump only if grounded
        if (!isGrounded) return; 
        rb.AddForce(Vector3.up * jumpStrength);
    }

    /* Called when "THE PLAYER" presses the spotlight toggle button (F)
    public void onSpotlight(CallbackContext ctx)
    {

        // Toggles Spotlight on/off
        if (ItemAtPlayer.activeSelf)
        {
            SpotLight.SetActive(!SpotLight.activeSelf);
        }
    }*/

    // -> Optional FOV zoom
    public void onFocus(CallbackContext ctx)
    {
        //Debug.Log("RMouse");
        //RealizeIt !!
    } 

    /* Called when "THE PLAYER" presses the inventory Key (I)
    public void onInventory(CallbackContext ctx)
    {

        // Check if the input action was just performed (e.g., key was pressed)
        if (ctx.performed)
        {

            // Make sure the inventory panel reference is assigned
            if (inventoryPanel != null)
            {

                // Check whether the inventory panel is currently active
                bool isActive = inventoryPanel.activeSelf;

                // Toggles the inventory UI Panel
                inventoryPanel.SetActive(!isActive);
            }
        }
    }*/

    // Check for ground contact to allow jumping
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // Called when the player leaves contact with a collider
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void FixedUpdate()
    {

        // Calculate movement direction relative to the camera
        var movementDirection = cameraTransform.right * movementInput.x + cameraTransform.forward * movementInput.y;

        // Ensure movement is restricted to the horizontal plane
        movementDirection = Vector3.ProjectOnPlane (movementDirection, Vector3.up).normalized;

        // Move´s the player based on direction, time, and movement speed
        transform.Translate(movementDirection * Time.deltaTime * actualMovementSpeed);
    }
}
