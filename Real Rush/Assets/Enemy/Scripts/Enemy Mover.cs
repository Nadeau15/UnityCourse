using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
  [SerializeField] List<Waypoint> path = new List<Waypoint>();
  [SerializeField] [Range(0f, 5f)] float movSpeed = 1f;

  Vector3 enemyPosition;
  
  void Start() {
    InitEnemyPosition();
    StartCoroutine(FollowPath());
  }

  IEnumerator FollowPath() {
    foreach(Waypoint waypoint in path) {
      Vector3 startPosition = transform.position;
      Vector3 endPosition = waypoint.transform.position;
      float travelPercent = 0f;

      transform.LookAt(endPosition);

      while (travelPercent < 1) {
        travelPercent += Time.deltaTime * movSpeed;
        transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
        enemyPosition = transform.position;
        yield return new WaitForEndOfFrame();
      }
    }
  }

  void InitEnemyPosition() {
    transform.position = path[0].transform.position;
    enemyPosition = transform.position;
  }
}
