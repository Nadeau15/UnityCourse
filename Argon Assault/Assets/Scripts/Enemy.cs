using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] GameObject deathParticles;
  [SerializeField] GameObject hitParticles;
  [SerializeField] int killReward = 10;
  [SerializeField] int hitPoints = 1;

  Scoreboard scoreboard;
  GameObject parentGameObject;

  void Start() {
    scoreboard = FindObjectOfType<Scoreboard>();
    parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
  }

  void OnParticleCollision(GameObject other) {
    ProcessHit();

    if (hitPoints < 1) {
      ProcessKill();
    } else {

    }
  }

  void ProcessHit() {
    GameObject vfx = Instantiate(hitParticles, transform.position, Quaternion.identity);
    AudioSource audioSource = vfx.GetComponent<AudioSource>();

    hitPoints--;
    vfx.transform.parent = parentGameObject.transform;
    if (!audioSource.isPlaying) {
      audioSource.enabled = true;
    }
  }

  void ProcessKill() {
    GameObject vfx = Instantiate(deathParticles, transform.position, Quaternion.identity);
    AudioSource audioSource = vfx.GetComponent<AudioSource>();

    vfx.transform.parent = parentGameObject.transform;
    scoreboard.IncreaseScore(killReward);
    audioSource.enabled = true;
    Destroy(gameObject);
  }
}
