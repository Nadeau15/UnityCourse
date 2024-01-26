using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 2;
    
    [Tooltip("Add amount to maxHitPoints when enemy dies")]
    [SerializeField] [Range(1, 5)] int difficultyRamp = 1;

    int currentHitPoints;
    Enemy enemy;

    void OnEnable() {
      currentHitPoints = maxHitPoints;
    }

    void Start() {
      enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other) {
      ProcessHit();
    }

    void ProcessHit() {
      currentHitPoints--;
      ProcessKill();
    }

    void ProcessKill() {
      if (currentHitPoints < 1) {
        gameObject.SetActive(false);
        maxHitPoints += difficultyRamp;
        enemy.RewardGold();
      }
    }
}
