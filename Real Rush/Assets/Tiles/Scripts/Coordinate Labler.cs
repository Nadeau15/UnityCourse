using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabler : MonoBehaviour
{
  [SerializeField] Color defaultColor =  Color.white;
  [SerializeField] Color blockedColor = Color.gray;

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  Waypoint waypoint;

  void Awake() {
    label = GetComponent<TextMeshPro>();
    label.enabled = false;
    waypoint = GetComponentInParent<Waypoint>();
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
    float snapSettingX = UnityEditor.EditorSnapSettings.move.x;
    float snapSettingZ = UnityEditor.EditorSnapSettings.move.z;

    coordinates.x = Mathf.RoundToInt(transform.parent.position.x / snapSettingX);
    coordinates.y = Mathf.RoundToInt(transform.parent.position.z / snapSettingZ);

    label.text = coordinates.ToString();
  }

  void SetLabelColor() {
    label.color = waypoint.IsPlaceable ? defaultColor : blockedColor;
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
