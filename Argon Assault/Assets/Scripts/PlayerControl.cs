using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
  CustomInput input = null;
  Vector2 movement = Vector2.zero;
  [SerializeField] float movSpeed = 15f;
  [SerializeField] float xRange = 32f;
  [SerializeField] float yMax = 14f;
  [SerializeField] float yMin = -6f;
  [SerializeField] float positionPitchFactor = -1f;
  [SerializeField] float positionYawFactor = 1f;
  [SerializeField] float controlPitchFactor = -250f;
  [SerializeField] float controlRollFactor = -250f;

  void Awake()
  {
    input = new CustomInput();
  }

  void OnEnable() {
    input.Enable();
    // Subscribe to function;
    input.Player.Movement.performed += OnMovementPerformed;
    input.Player.Movement.canceled += OnMovementCanceled;
  }

  void OnDisable() {
    input.Disable();
    // Unsubscribe
    input.Player.Movement.performed -= OnMovementPerformed;
    input.Player.Movement.canceled += OnMovementCanceled;
  }

  void OnMovementPerformed(InputAction.CallbackContext value) {
    movement = value.ReadValue<Vector2>() * movSpeed * Time.deltaTime;
  }

  void OnMovementCanceled(InputAction.CallbackContext value) {
    movement = Vector2.zero;
  }

  void Update() {
    ProcessTranslation();
    ProcessRotation();
  }

  void ProcessRotation() {
    float positionPitch = transform.localPosition.y * positionPitchFactor;
    float controlPitch = movement.y * controlPitchFactor;
    float pitch = positionPitch + controlPitch; 
    
    float yaw = transform.localPosition.x * positionYawFactor;
    float roll = movement.x * controlRollFactor;

    transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
  }

  void ProcessTranslation() {
    float localPositionX = transform.localPosition.x;
    float localPositionY = transform.localPosition.y;
    float localPositionZ = transform.localPosition.z;

    float newXPos = localPositionX + movement.x;
    float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

    float newYPos = localPositionY + movement.y;
    float clampedYPos = Mathf.Clamp(newYPos, yMin, yMax);

    transform.localPosition = new Vector3(clampedXPos, clampedYPos, localPositionZ);
  }
}
