using UnityEngine;

public class Waypoint : MonoBehaviour {
  [SerializeField] bool isPlaceable;
  [SerializeField] GameObject towerPrefab;


  void OnMouseDown() {
    if (isPlaceable) {
      Instantiate(towerPrefab, transform.position, Quaternion.identity);
      isPlaceable = false;
    }
  }
}
