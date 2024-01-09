using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scorer : MonoBehaviour
{
  List<string> objectCollided = new List<string>();
  int score = 100;
  // Start is called before the first frame update
  void Start() {
    Debug.Log("Starting score: " + score);
  }

  private void OnCollisionEnter(Collision other) {
    string objectName = other.gameObject.name.ToLower();
    if ((objectName.Contains("wall") || objectName.Contains("obstacle")) && !objectCollided.Contains(objectName)) {
      score -= 10;
      objectCollided.Add(objectName);
      Debug.Log("Score: " + score);
    }

    if (score == 0) {
      Debug.Log("Game Over");
    }
  }
}
