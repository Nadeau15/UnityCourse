using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float nextLevelDelay = 2f;
  [SerializeField] float reloadDelay = 6f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles; 

  Movement movement;
  AudioSource audioSource;

  bool isTransitioning = false;

  void Start() {
    movement = GetComponent<Movement>();
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other) {
    if (!isTransitioning) {
      switch(other.gameObject.tag) {
        case "Friendly":
          Debug.Log("Friendly case");
          break;
        case "Finish":
          StartFinishSequence();
          break;
        default:
          StartCrashSequence();
          break;
      }
    }
  }

  void StartCrashSequence() {
    isTransitioning = true;
    movement.enabled = false;
    audioSource.Stop();
    StopMovementParticles();
    crashParticles.Play();
    audioSource.PlayOneShot(crash);

    Invoke("ReloadLevel", reloadDelay);
  }

  void StartFinishSequence() {
    isTransitioning = true;
    movement.enabled = false;
    audioSource.Stop();
    StopMovementParticles();
    successParticles.Play();
    audioSource.PlayOneShot(success);

    Invoke("LoadNextLevel", nextLevelDelay);
  }

  void ReloadLevel() {
    int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(activeSceneIndex);
  }

  void LoadNextLevel() {
    int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = (activeSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

    SceneManager.LoadScene(nextSceneIndex);
  }

  void StopMovementParticles() {
    movement.leftThrusterParticles.Stop();
    movement.rightThrusterParticles.Stop();
    movement.mainThrusterParticles.Stop();
  }
}
