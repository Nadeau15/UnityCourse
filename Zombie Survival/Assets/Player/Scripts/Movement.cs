using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] float movSpeed;
  [SerializeField] float sprintSpeed;
  [SerializeField] float groundDrag;
  [SerializeField] float jumpForce;
  [SerializeField] float airMultiplier;

  [Header("Keybinds")]
  [SerializeField] KeyCode jumpKey = KeyCode.Space;
  [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

  [Header("Ground Check")]
  [SerializeField] float playerHeight;

  [SerializeField] Transform orientation;

  bool grounded;

  float currentMovSpeed;
  float horizontalInput;
  float verticalInput;

  Vector3 movDirection;

  Rigidbody rb;

  void Start() {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }

  void Update() {
    // Ground check
    grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

    MyInput();
    SpeedControl();

    // handle drag
    if (grounded) {
      rb.drag = groundDrag;
    } else {
      rb.drag = 0;
    }
  }

  void FixedUpdate() {
    MovePlayer();
  }

  void MyInput() {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    if (Input.GetKey(jumpKey) && grounded) {
      Jump();
    }

    currentMovSpeed = Input.GetKey(sprintKey) ? sprintSpeed : movSpeed;
  }

  void MovePlayer() {
      movDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

      if (grounded) {
        rb.AddForce(movDirection.normalized * currentMovSpeed * 10f, ForceMode.Force);
      } else if (!grounded) {
        rb.AddForce(movDirection.normalized * currentMovSpeed * 10f * airMultiplier, ForceMode.Force);
      }
  }

  void SpeedControl() {
    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    if (flatVel.magnitude > currentMovSpeed) {
      Vector3 limitedVel = flatVel.normalized * currentMovSpeed;
      rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    }
  }

  void Jump() {
    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
  }
}
