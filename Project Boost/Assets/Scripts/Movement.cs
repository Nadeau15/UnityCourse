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
      StartThrusting();
    } else {
      StopThrusting();
    }
  }

  void ProcessRotation() {
    if (Input.GetKey(KeyCode.A)) {
      RotateLeft();
    } else if (Input.GetKey(KeyCode.D)) {
      RotateRight();
    } else {
      StopRotation();
    }
  }

  void StartThrusting() {
    rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(thrusters);
    }

    if (!mainThrusterParticles.isPlaying) {
      mainThrusterParticles.Play();
    }
  }

  void StopThrusting() {
    mainThrusterParticles.Stop();
    audioSource.Stop();
  }

  void RotateRight() {
    ApplyRotation(-Vector3.forward);
    if (!leftThrusterParticles.isPlaying) {
      leftThrusterParticles.Play();
    }
  }

  void RotateLeft() {
    ApplyRotation(Vector3.forward);
    if (!rightThrusterParticles.isPlaying)
    {
      rightThrusterParticles.Play();
    }
  }

  void StopRotation() {
    rightThrusterParticles.Stop();
    leftThrusterParticles.Stop();
  }

  void ApplyRotation(Vector3 direction) {
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
