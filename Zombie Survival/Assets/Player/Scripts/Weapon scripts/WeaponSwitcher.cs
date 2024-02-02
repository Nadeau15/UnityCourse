using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
  [SerializeField] int currentWeapon = 1;

  void Start() {
    SetWeaponActive();
  }

  void Update() {
    int previousWeapon = currentWeapon;

    ProcessKeyInput();
    ProcessScrollInput();

    if (previousWeapon != currentWeapon) {
      SetWeaponActive();
    }
  }

  void ProcessKeyInput() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      currentWeapon = 1;
    }

    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      currentWeapon = 2;
    }

    if (Input.GetKeyDown(KeyCode.Alpha3)) {
      currentWeapon = 3;
    }

    if (Input.GetKeyDown(KeyCode.Alpha4)) {
      currentWeapon = 4;
    }
  }

  void ProcessScrollInput() {
    if (Input.GetAxis("Mouse ScrollWheel") < 0) {
      if (currentWeapon >= transform.childCount) {
        currentWeapon = 1;
      } else {
        currentWeapon++;
      }
    }

    if (Input.GetAxis("Mouse ScrollWheel") > 0) {
      if (currentWeapon <= 1) {
        currentWeapon = transform.childCount;
      } else {
        currentWeapon--;
      }
    }
  }

  void SetWeaponActive() {
    int weaponIndex = 1;

    foreach(Transform weapon in transform) {
      if (weaponIndex == currentWeapon) {
        weapon.gameObject.SetActive(true);
      } else {
        weapon.gameObject.SetActive(false);
      }

      weaponIndex++;
    }
  }
}
