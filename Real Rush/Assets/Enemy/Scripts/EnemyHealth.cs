using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 3;

    int currentHitPoints;

    void Start() {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
      Debug.Log(other.gameObject.name);
      ProcessHit();
    }

    void ProcessHit() {
      currentHitPoints--;
      ProcessKill();
    }

    void ProcessKill() {
      if (currentHitPoints < 1) {
        Destroy(gameObject);
      }
    }
}
