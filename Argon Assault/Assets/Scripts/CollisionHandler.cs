using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float reloadDelay = 1f; 
  [SerializeField] GameObject crashParticles;
  [SerializeField] Transform parent;
  PlayerControl playerControl;
  bool crashTransitionInProgress = false;

  void Start() {
    playerControl = GetComponent<PlayerControl>();
  }

  void OnTriggerEnter(Collider other) {
    StartCrashSequence();
  }

  void StartCrashSequence() {
    if (!crashTransitionInProgress) {
      GameObject vfx = Instantiate(crashParticles, transform.position, Quaternion.identity);
      AudioSource audioSource = vfx.GetComponent<AudioSource>();

      playerControl.enabled = false;
      crashTransitionInProgress = true;
      gameObject.SetActive(false);
      vfx.transform.parent = parent;
      audioSource.enabled = true;
      Invoke("ReloadLevel", reloadDelay);
    }
  }

  void ReloadLevel() {
    int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(activeSceneIndex);
  }
}
