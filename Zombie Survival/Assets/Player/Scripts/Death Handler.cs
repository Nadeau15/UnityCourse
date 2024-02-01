using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
  [SerializeField] Canvas gameOverCanvas;

  void Start() {
    gameOverCanvas.enabled = false;
  }

  public void HandleDeath() {
    GetComponent<Movement>().enabled = false;
    gameOverCanvas.enabled = true;
    Time.timeScale = 0;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }
}
