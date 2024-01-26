using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField] GameObject enemy;
  [SerializeField] [Range(0.1f, 30f)] float spawnDelay = 1f;
  [SerializeField] [Range(0, 50)] int poolSize = 5;

  GameObject[] pool;

  void Awake() {
    PopulatePool();
  }

  void Start() {
    StartCoroutine(EnemyIntantiator());
  }

  void PopulatePool() {
    pool = new GameObject[poolSize];

    for(int i = 0; i < pool.Length; i++) {
      pool[i] = Instantiate(enemy, transform);
      pool[i].SetActive(false);
    }
  }

  void EnableObjectInPool() {
    foreach(GameObject gameObject in pool) {
      if(!gameObject.activeInHierarchy) {
        gameObject.SetActive(true);
        return;
      }
    }
  }

  IEnumerator EnemyIntantiator() {
    while(Application.isPlaying) {
      EnableObjectInPool();
      yield return new WaitForSeconds(spawnDelay);
    }
  }
}
