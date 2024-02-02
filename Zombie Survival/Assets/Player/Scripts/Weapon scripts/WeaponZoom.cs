using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
  [SerializeField] Camera followCamera;
  [SerializeField] float zoomedOutFOV = 40f;
  [SerializeField] float zoomedInFOV = 25f;
  [SerializeField] float defaultRotationSpeed = 1f;
  [SerializeField] float zoomedInRotationSpeed = 0.5f;
  FPSController fPSController;

  void Awake() {
    fPSController = FindObjectOfType<FPSController>();
  }

  void Update() {
    if (Input.GetMouseButton(1)) {
      followCamera.fieldOfView = zoomedInFOV;
      fPSController.lookSensitivity = zoomedInRotationSpeed;
    } else {
      followCamera.fieldOfView = zoomedOutFOV;
      fPSController.lookSensitivity = defaultRotationSpeed;
    }
  }
}
