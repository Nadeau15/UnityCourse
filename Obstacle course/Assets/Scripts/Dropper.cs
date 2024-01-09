using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
  // Components
  MeshRenderer meshRenderer;
  Rigidbody rigidBody;

  [SerializeField] float waitingTime = 2;

  // Start is called before the first frame update
  void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
    rigidBody = GetComponent<Rigidbody>();

    meshRenderer.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time > waitingTime) {
      meshRenderer.enabled = true;
      rigidBody.useGravity = true;
    } else if (Time.time > waitingTime + Time.deltaTime) {
      rigidBody.useGravity = false;
      rigidBody.constraints = RigidbodyConstraints.FreezePositionY;
    }
  }
}
