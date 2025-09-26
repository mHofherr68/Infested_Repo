/*using UnityEngine;

public class WeaponSwayFromCamera : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Wenn leer: Camera.main wird verwendet")]
    [SerializeField] private Transform cameraTransform;

    [Header("Sway Settings")]
    [Tooltip("Skalierung der Sway-Reaktion (Größe des Nachschwingens)")]
    [SerializeField] private float swayMultiplier = 0.15f;
    [Tooltip("Glättung der Sway-Bewegung (höher = träger, weicher)")]
    [SerializeField] private float smoothing = 8f;
    [Tooltip("Maximaler Winkel (in Grad) pro Achse")]
    [SerializeField] private float maxAngle = 6f;
    [Tooltip("Wie stark Y-Achsänderung in Roll (Z) umgewandelt wird")]
    [SerializeField][Range(0f, 1f)] private float rollFactor = 0.5f;

    [Header("Optional")]
    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;

    private Vector3 prevCamEuler;
    private Vector3 currentSwayEuler;
    private Quaternion startLocalRot;

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        startLocalRot = transform.localRotation;
        if (cameraTransform != null)
            prevCamEuler = cameraTransform.eulerAngles;
        else
            prevCamEuler = Vector3.zero;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 camEuler = cameraTransform.eulerAngles;

        float deltaYaw = Mathf.DeltaAngle(prevCamEuler.y, camEuler.y);
        float deltaPitch = Mathf.DeltaAngle(prevCamEuler.x, camEuler.x);
        prevCamEuler = camEuler;

        float sx = (invertY ? 1f : -1f) * deltaPitch * swayMultiplier;
        float sy = (invertX ? -1f : 1f) * deltaYaw * swayMultiplier;
        float sz = sy * rollFactor;

        sx = Mathf.Clamp(sx, -maxAngle, maxAngle);
        sy = Mathf.Clamp(sy, -maxAngle, maxAngle);
        sz = Mathf.Clamp(sz, -maxAngle, maxAngle);

        Vector3 targetSway = new Vector3(sx, sy, sz);

        currentSwayEuler = Vector3.Lerp(currentSwayEuler, targetSway, Time.deltaTime * smoothing);

        transform.localRotation = startLocalRot * Quaternion.Euler(currentSwayEuler);
    }

    public void ResetStartRotation()
    {
        startLocalRot = transform.localRotation;
        currentSwayEuler = Vector3.zero;
    }
}*/
using UnityEngine;

/// <summary>
/// Applies a subtle sway effect to a weapon (or any child object of the camera) 
/// based on the rotation changes of the player camera. 
/// This creates the illusion that the weapon lags slightly behind when turning.
/// </summary>
public class WeaponSwayFromCamera : MonoBehaviour
{
    [Header("Select Camera Reference")]
    [Space(16)]

    [SerializeField] private Transform cameraTransform;

    [Header("Sway Settings")]
    [Space(16)]

    [SerializeField] private float swayMultiplier = 0.15f;

    [SerializeField] private float smoothing = 8f;

    [SerializeField] private float maxAngle = 6f;

    [SerializeField][Range(0f, 1f)] private float rollFactor = 0.5f;

    [SerializeField] private bool invertX = false;

    [SerializeField] private bool invertY = false;

    // The previous camera Euler rotation, used to calculate delta changes each frame.
    private Vector3 prevCamEuler;

    // The current smoothed sway rotation offset, in Euler angles.
    private Vector3 currentSwayEuler;

    // The starting local rotation of the weapon, used as a base to apply sway offsets.
    private Quaternion startLocalRot;

    private void Awake()
    {
        // Automatically assign Camera.main if no camera reference is set.
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Store the weapon's starting local rotation (important since this is a child of the camera).
        startLocalRot = transform.localRotation;

        // Initialize previous camera rotation to avoid a large jump on the first frame.
        if (cameraTransform != null)
            prevCamEuler = cameraTransform.eulerAngles;
        else
            prevCamEuler = Vector3.zero;
    }

    private void LateUpdate()
    {
        // Do nothing if no camera is assigned.
        if (cameraTransform == null) return;

        // Get current camera Euler rotation.
        Vector3 camEuler = cameraTransform.eulerAngles;

        // Calculate rotation changes (delta angles) since the last frame.
        float deltaYaw = Mathf.DeltaAngle(prevCamEuler.y, camEuler.y);   // Horizontal turn
        float deltaPitch = Mathf.DeltaAngle(prevCamEuler.x, camEuler.x); // Vertical look
        prevCamEuler = camEuler;

        // Apply multipliers and optional inversion.
        float sx = (invertY ? 1f : -1f) * deltaPitch * swayMultiplier; // Pitch sway
        float sy = (invertX ? -1f : 1f) * deltaYaw * swayMultiplier;   // Yaw sway
        float sz = sy * rollFactor;                                    // Roll sway from yaw

        // Clamp sway angles so it never looks exaggerated.
        sx = Mathf.Clamp(sx, -maxAngle, maxAngle);
        sy = Mathf.Clamp(sy, -maxAngle, maxAngle);
        sz = Mathf.Clamp(sz, -maxAngle, maxAngle);

        // Combine into a target sway vector.
        Vector3 targetSway = new Vector3(sx, sy, sz);

        // Smoothly interpolate towards the target sway to make the motion feel natural.
        currentSwayEuler = Vector3.Lerp(currentSwayEuler, targetSway, Time.deltaTime * smoothing);

        // Apply sway relative to the weapon's starting local rotation.
        transform.localRotation = startLocalRot * Quaternion.Euler(currentSwayEuler);
    }

    /// <summary>
    /// Resets the starting local rotation and clears sway.
    /// Call this if the weapon model changes or is re-equipped.
    /// </summary>
    public void ResetStartRotation()
    {
        startLocalRot = transform.localRotation;
        currentSwayEuler = Vector3.zero;
    }
}

