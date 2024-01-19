using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabler : MonoBehaviour
{
  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();

  void Awake() {
    label = GetComponent<TextMeshPro>();
    DisplayCoordinates();
  }

  void Update()
  {
    if (!Application.IsPlaying(gameObject)) {
      DisplayCoordinates();
      UpdateObjectName();
    }
  }

  void DisplayCoordinates() {
    float snapSettingX = UnityEditor.EditorSnapSettings.move.x;
    float snapSettingZ = UnityEditor.EditorSnapSettings.move.z;

    coordinates.x = Mathf.RoundToInt(transform.parent.position.x / snapSettingX);
    coordinates.y = Mathf.RoundToInt(transform.parent.position.z / snapSettingZ);

    label.text = coordinates.ToString();
  }

  void UpdateObjectName() {
    transform.parent.name = coordinates.ToString();
  }
}
