using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
  CustomInput input = null;
  Vector2 movement = Vector2.zero;

  [Header("General Setup Settings")]
  [Tooltip("How fast ship moves based on player input")] [SerializeField] float movSpeed = 15f;

  [Header("Movement range on screen")]
  [SerializeField] float xRange = 32f;
  [SerializeField] float yMax = 14f;
  [SerializeField] float yMin = -6f;

  [Header("Screen position based tuning")]
  [SerializeField] float positionPitchFactor = -1f;
  [SerializeField] float positionYawFactor = 1f;

  [Header("Player input based tuning")]

  [SerializeField] float controlPitchFactor = -250f;
  [SerializeField] float controlRollFactor = -250f;

  [Header("Laser gun array")]
  [SerializeField] GameObject[] lasers;

  void Awake()
  {
    input = new CustomInput();
  }

  void OnEnable() {
    input.Enable();
    // Subscribe to function;
    input.Player.Movement.performed += OnMovementPerformed;
    input.Player.Movement.canceled += OnMovementCanceled;

    input.Player.Fire.performed += OnFirePerformed;
    input.Player.Fire.canceled += OnFireCanceled;
  }

  void OnDisable() {
    input.Disable();
    // Unsubscribe
    input.Player.Movement.performed -= OnMovementPerformed;
    input.Player.Movement.canceled -= OnMovementCanceled;

    input.Player.Fire.performed -= OnFirePerformed;
    input.Player.Fire.canceled -= OnFireCanceled;
  }

  void OnMovementPerformed(InputAction.CallbackContext value) {
    movement = value.ReadValue<Vector2>() * movSpeed * Time.deltaTime;
  }

  void OnMovementCanceled(InputAction.CallbackContext value) {
    movement = Vector2.zero;
  }

  void OnFirePerformed(InputAction.CallbackContext value) {
    ActivateLasers(true);
  }

  void OnFireCanceled(InputAction.CallbackContext value) {
    ActivateLasers(false);
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

  void ActivateLasers(bool isActive) {
    foreach (var laser in lasers) {
      var emissionModule = laser.GetComponent<ParticleSystem>().emission;
      emissionModule.enabled = isActive;
    }
  }
}
