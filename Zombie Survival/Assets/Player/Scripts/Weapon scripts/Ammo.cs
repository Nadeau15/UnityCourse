using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
  [SerializeField] AmmoSlot[] ammoSlots;
  
  [System.Serializable]
  private class AmmoSlot {
    public WeaponType weaponType;
    public int ammoAmount;
  }

  AmmoSlot GetAmmoSlot(WeaponType weaponType) {
    foreach(AmmoSlot slot in ammoSlots) {
      if (slot.weaponType == weaponType) {
        return slot;
      }
    }
    return null;
  }

  public int GetAmmoCount(WeaponType weaponType) {
    return GetAmmoSlot(weaponType).ammoAmount;
  }

  public void ReduceAmmoCount(WeaponType weaponType) {
    GetAmmoSlot(weaponType).ammoAmount--;
  }
}
