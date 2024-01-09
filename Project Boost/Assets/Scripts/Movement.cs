using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  Rigidbody rb;
  [SerializeField] float thrustSpeed = 1000f;
  [SerializeField] float rotationSpeed = 1000f;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
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
    }
  }

  void ProcessRotation() {
    if (Input.GetKey(KeyCode.A)) {
      ApplyRotation(Vector3.forward);
    } else if (Input.GetKey(KeyCode.D)) {
      ApplyRotation(-Vector3.forward);
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
