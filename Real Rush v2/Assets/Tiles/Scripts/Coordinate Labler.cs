using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabler : MonoBehaviour
{
  [SerializeField] Color defaultColor =  Color.white;
  [SerializeField] Color blockedColor = Color.gray;
  [SerializeField] Color exploredColor =  Color.yellow;
  [SerializeField] Color pathColor = new Color(1, 0.5f, 0);

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  GridManager gridManager;

  void Awake() {
    gridManager = FindObjectOfType<GridManager>();
    label = GetComponent<TextMeshPro>();
    label.enabled = false;
    DisplayCoordinates();
  }

  void Update()
  {
    if (!Application.IsPlaying(gameObject)) {
      DisplayCoordinates();
      UpdateObjectName();
      label.enabled = true;
    }

    SetLabelColor();
    ToggleLabels();
  }

  void DisplayCoordinates() {
    if (gridManager != null) {
      coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
      coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

      label.text = coordinates.ToString();
    }
  }

  void SetLabelColor() {
    Node node = null;
    if (gridManager != null) {
      node = gridManager.GetNode(coordinates);
    }

    if(node != null) {
      if(!node.isWalkable) {
        label.color = blockedColor;
      } else if(node.isPath) {
        label.color = pathColor;
      } else if(node.isExplored) {
        label.color = exploredColor;
      } else {
        label.color = defaultColor;
      }
    }
  }

  void ToggleLabels() {
    if (Input.GetKeyDown(KeyCode.C)) {
      label.enabled = !label.IsActive();
    }
  }

  void UpdateObjectName() {
    transform.parent.name = coordinates.ToString();
  }
}
