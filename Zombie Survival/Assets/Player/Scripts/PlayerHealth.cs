using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] float maxHp = 100f;

  float currentHp;

  void Awake() {
    currentHp = maxHp;
  }

  public void TakeDamage(float damageTaken) {
    currentHp -= damageTaken;

    if (currentHp < 1) {
      GetComponent<DeathHandler>().HandleDeath();
    }
  }
}
