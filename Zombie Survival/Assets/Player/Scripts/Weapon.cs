using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField] Camera FPCamera;
  [SerializeField] float range = 100f;
  [SerializeField] float weaponDamage = 25f;
  [SerializeField] float headshotDamageMultiplier = 2f;
  [SerializeField] ParticleSystem muzzleFlash;

  void Update()
  {
    if(Input.GetButtonDown("Fire1")) {
      Shoot();
    }
  }

  void Shoot() {
    PlayMuzzleFlash();
    ProcessRaycast();
  }

    void PlayMuzzleFlash() {
      muzzleFlash.Play();
    }

    void ProcessRaycast()
  {
    RaycastHit hit;

    if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
    {
      float damageDealth = weaponDamage;
      // TODO: add hit effects
      EnemyHealth targetHealth = hit.transform.GetComponentInParent<EnemyHealth>();
      if (targetHealth == null) {
        return;
      };

      if (hit.transform.tag == "EnemyHead") {
        damageDealth *= headshotDamageMultiplier;
      }

      targetHealth.TakeDamage(damageDealth);
    }
  }
}
