using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{
  [SerializeField] Transform weapon;

  Transform target;
  
  void Start() {
    target = FindObjectOfType<EnemyMover>().transform;
  }

  void OnCollisionEnter(Collision other) {
    Debug.Log("Collision with " + other.gameObject.name);
  }

  void Update() {
    AimWeapon();
  }

  void AimWeapon() {
    weapon.LookAt(target);
  }
}
