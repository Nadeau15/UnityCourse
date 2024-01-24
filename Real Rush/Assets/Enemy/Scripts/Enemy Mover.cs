using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
  [SerializeField] List<Waypoint> path = new List<Waypoint>();
  [SerializeField] [Range(0f, 5f)] float movSpeed = 1f;
  Enemy enemy;
  
  void OnEnable() {
    FindPath();
    InitEnemyPosition();
    StartCoroutine(FollowPath());
  }

  void Start() {
    enemy = GetComponent<Enemy>();
  }

  void FindPath() {
    path.Clear();

    GameObject parent = GameObject.FindGameObjectWithTag("Path");

    foreach(Transform child in parent.transform) {
      Waypoint waypoint = child.GetComponent<Waypoint>();

      if(waypoint != null) {
        path.Add(child.GetComponent<Waypoint>());
      }
    }
  }

  void FinishPath() {
    enemy.StealGold();
    gameObject.SetActive(false);
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
        yield return new WaitForEndOfFrame();
      }
    }
    FinishPath();
  }

  void InitEnemyPosition() {
    transform.position = path[0].transform.position;
    Vector3 nextPosition = path[1].transform.position;

    transform.LookAt(nextPosition);
  }
}
