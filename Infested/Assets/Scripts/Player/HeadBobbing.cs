using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Head Bobbing Settings")]
    public float bobFrequency = 1.5f;
    public float bobHorizontalAmount = 0.05f;
    public float bobVerticalAmount = 0.05f;

    public BaseCharacterController controller;

    private Vector3 startPos;
    private float timer = 0;

    void Start()
    {
        startPos = transform.localPosition;

        if (controller == null)
        {
            controller = GetComponentInParent<BaseCharacterController>();
        }
    }

    void Update()
    {
        bool isMoving = controller != null
                      && controller.isGrounded
                      && controller.movementInput.magnitude > 0.1f;

        if (isMoving)
        {
            // Geschwindigkeit berücksichtigen
            float speedFactor = (controller.actualMovementSpeed + controller.externalSpeedOffset) / controller.actualMovementSpeed;

            timer += Time.deltaTime * bobFrequency * speedFactor;

            float bobX = Mathf.Sin(timer) * bobHorizontalAmount;
            float bobY = Mathf.Cos(timer * 2) * bobVerticalAmount;

            Vector3 horizontalOffset = transform.right * bobX;
            Vector3 verticalOffset = transform.up * bobY;

            transform.localPosition = startPos + horizontalOffset + verticalOffset;
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 5);
        }
    }
}
