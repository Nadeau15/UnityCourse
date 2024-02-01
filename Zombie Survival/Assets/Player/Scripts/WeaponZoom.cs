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
  [SerializeField] float defaultRotationSpeed = 200f;
  [SerializeField] float zoomedInRotationSpeed = 100f;
  PlayerCam playerCam;

  void Awake() {
    playerCam = GetComponent<PlayerCam>();
  }

  void Update() {
    if (Input.GetMouseButton(1)) {
      followCamera.fieldOfView = zoomedInFOV;
      playerCam.sensitivityX = zoomedInRotationSpeed;
      playerCam.sensitivityY = zoomedInRotationSpeed;
    } else {
      followCamera.fieldOfView = zoomedOutFOV;
      playerCam.sensitivityX = defaultRotationSpeed;
      playerCam.sensitivityY = defaultRotationSpeed;
    }
  }
}
