using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
  public Camera playerCamera;

  [Header("Controls")]
  public KeyCode jumpKey = KeyCode.Space;
  public KeyCode runKey = KeyCode.LeftShift;

  [Header("Movement")]
  public float walkSpeed = 6f;
  public float runSpeed = 12f;
  public float jumpForce = 7f;
  public float lookSensitivity = 1f;
  public float lookXLimit = 45f;
  public bool canMove = true;

  [Header("Physics")]
  public float gravity = 10f;

  CharacterController characterController;
  Vector3 moveDirection = Vector3.zero;
  float rotationX = 0;

  void Start() {
    characterController = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    Vector3 forward = transform.TransformDirection(Vector3.forward);
    Vector3 right = transform.TransformDirection(Vector3.right);

    // Movement
    bool isRunning = Input.GetKey(runKey);
    float currentSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
    float currentSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
    float movementDirectionY = moveDirection.y;
    moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

    // Jumping
    if (Input.GetKey(jumpKey) && canMove && characterController.isGrounded) {
      moveDirection.y = jumpForce;
    } else {
      moveDirection.y = movementDirectionY;
    }

    if (!characterController.isGrounded) {
      moveDirection.y -= gravity * Time.deltaTime;
    }

    // Rotation
    characterController.Move(moveDirection * Time.deltaTime);

    if (canMove) {
      rotationX += -Input.GetAxis("Mouse Y") * lookSensitivity;
      rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
      playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

      transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
    }
  }
}
