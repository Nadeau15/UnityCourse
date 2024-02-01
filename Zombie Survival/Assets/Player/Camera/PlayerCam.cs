using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
  [SerializeField] Transform orientation;

  public float sensitivityX;
  public float sensitivityY;

  float xRotation;
  float yRotation;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    // get mouse input
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

    yRotation += mouseX;
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    // Rotate cam and orientation
    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
  }
}
