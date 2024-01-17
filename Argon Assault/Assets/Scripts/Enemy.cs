using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] GameObject deathParticles;
  [SerializeField] Transform parent;
  [SerializeField] int killReward = 10;

  Scoreboard scoreboard;

  void Start() {
    scoreboard = FindObjectOfType<Scoreboard>();
  }

  void OnParticleCollision(GameObject other) {
    ProcessHit();
  }

  void ProcessHit() {
    GameObject vfx = Instantiate(deathParticles, transform.position, Quaternion.identity);
    vfx.transform.parent = parent;
    scoreboard.IncreaseScore(killReward);
    Destroy(gameObject);
  }
}
