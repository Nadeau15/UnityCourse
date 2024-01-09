using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float thrustSpeed = 1000f;
  [SerializeField] float rotationSpeed = 1000f;
  [SerializeField] AudioClip thrusters;
  [SerializeField] public ParticleSystem mainThrusterParticles;
  [SerializeField] public ParticleSystem leftThrusterParticles;
  [SerializeField] public ParticleSystem rightThrusterParticles; 

  Rigidbody rb;
  AudioSource audioSource;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessInput();
  }

  void ProcessInput() {
    ProcessThrust();
    ProcessRotation();        
  }

  void ProcessThrust() {
    if (Input.GetKey(KeyCode.Space)) {
      rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
      if (!audioSource.isPlaying) {
        audioSource.PlayOneShot(thrusters);
      }

      if (!mainThrusterParticles.isPlaying) {
        mainThrusterParticles.Play();
      }
    } else {
      mainThrusterParticles.Stop();
      audioSource.Stop();
    }
  }

  void ProcessRotation() {
    if (Input.GetKey(KeyCode.A)) {
      ApplyRotation(Vector3.forward);
      if (!rightThrusterParticles.isPlaying) {
        rightThrusterParticles.Play();
      }
    } else if (Input.GetKey(KeyCode.D)) {
      ApplyRotation(-Vector3.forward);
      if (!leftThrusterParticles.isPlaying) {
        leftThrusterParticles.Play();
      }
    } else {
      rightThrusterParticles.Stop();
      leftThrusterParticles.Stop();
    }
  }

  void ApplyRotation(Vector3 direction)
  {
    // Freezing rotation in physic system to manually rotate the rocket 
    ToggleFreezeRotation();
    transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    ToggleFreezeRotation();
  }

  void ToggleFreezeRotation() {
    if (rb.freezeRotation) {
      rb.freezeRotation = false;
    } else {
      rb.freezeRotation = true;
    }
  }
}
