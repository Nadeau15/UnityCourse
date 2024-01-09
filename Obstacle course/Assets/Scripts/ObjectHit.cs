using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
  private void OnCollisionEnter(Collision other) {
    string otherObjectTag = other.gameObject.tag.ToLower();
    if (otherObjectTag == "player") {
      GetComponent<MeshRenderer>().material.color = Color.red;
    }
  }
}
