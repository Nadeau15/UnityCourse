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
  [SerializeField] GameObject woodHitParticleFX;
  [SerializeField] GameObject metalHitParticleFX;
  [SerializeField] GameObject fleshHitParticleFX;
  [SerializeField] GameObject sandHitParticleFX;
  [SerializeField] GameObject stoneHitParticleFX;
  [SerializeField] Ammo ammoSlot;
  [SerializeField] float rateOfFire = 0.5f;
  [SerializeField] WeaponType weaponType = WeaponType.Pistol;

  bool canShoot = true;

  void OnEnable() {
    canShoot = true;
  }

  void Update()
  {
    bool input = false;

    switch (weaponType) {
      case WeaponType.Pistol:
      case WeaponType.Shotgun:
      case WeaponType.Sniper:
        input = Input.GetButtonDown("Fire1");
        break;
      case WeaponType.AssaultRifle:
        input = Input.GetButton("Fire1");
        break;
    }

    if(input && ammoSlot.GetAmmoCount(weaponType) > 0 && canShoot) {
      StartCoroutine(Shoot());
    }
  }

  IEnumerator Shoot() {
    canShoot = false;

    PlayMuzzleFlash();
    ProcessRaycast();
    ammoSlot.ReduceAmmoCount(weaponType);

    yield return new WaitForSeconds(rateOfFire);
    canShoot = true;
  }

  void PlayMuzzleFlash() {
    muzzleFlash.Play();
  }

  void ProcessRaycast() {
    RaycastHit hit;

    if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
    {
      float damageDealth = weaponDamage;    

      ProcessHitParticles(hit);  

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

  void ProcessHitParticles(RaycastHit targetHit) {
    string targetMaterial = targetHit.transform.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
    GameObject impact = null;

    switch(targetMaterial) {
      case "WoodSurface":
        impact = Instantiate(woodHitParticleFX, targetHit.point, Quaternion.LookRotation(targetHit.normal));
        break;
      case "MetalSurface":
        impact = Instantiate(metalHitParticleFX, targetHit.point, Quaternion.LookRotation(targetHit.normal));
        break;
      case "SandSurface":
        impact = Instantiate(sandHitParticleFX, targetHit.point, Quaternion.LookRotation(targetHit.normal));
        break;
      case "FleshSurface":
        impact = Instantiate(fleshHitParticleFX, targetHit.point, Quaternion.LookRotation(targetHit.normal));
        break;
      case "StoneSurface":
        impact = Instantiate(stoneHitParticleFX, targetHit.point, Quaternion.LookRotation(targetHit.normal));
        break;
      default:
        Debug.LogWarning("No case Material for: " + targetMaterial);
        break;
    }

    if (impact != null) {
      impact.transform.SetParent(targetHit.transform);
      Destroy(impact, 5);
    }
  }
}
