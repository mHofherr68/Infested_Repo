using UnityEngine;

public class MoveEquipment : MonoBehaviour
{

    // Reference to the player's camera, used to move aligned Object (e.g. Flashlight, Weapon)
    [SerializeField] private Transform playerCam;

    // Positional offset of the equipment relative to the camera
    [SerializeField] private Vector3 equiPosOffset = new Vector3();

    // Rotational offset of the equipment relative to the camera
    [SerializeField] private Vector3 equiRotOffset = Vector3.zero;

    // Smoothing speed for equipment rotation
    [SerializeField] private float rotationSmoothSpeed;

    private void LateUpdate()
    {

        // Calculate the target rotation by applying the offset to the camera's current rotation
        Quaternion targetRotation = playerCam.rotation * Quaternion.Euler(equiRotOffset);

        // Smoothly interpolate the current rotation toward the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);

        // Set the equipment's position based on the camera's position plus the positional offset
        transform.position = playerCam.TransformPoint(equiPosOffset);
    }
}
