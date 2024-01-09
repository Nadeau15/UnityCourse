using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
  [SerializeField] float movementSpeed = 10f;

  // Start is called before the first frame update
  void Start() {
    PrintInstructions();
  }

  // Update is called once per frame
  void Update() {
    MovePlayer();
  }

  void PrintInstructions() {
    Debug.Log("Welcome to the game");
    Debug.Log("Move with WASD or arrow keys");
    Debug.Log("Don't it the walls");
  }

  void MovePlayer() {
    // Using Time.deltaTime make the movement speed frame independant
    float xPos = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
    float zPos = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

    transform.Translate(xPos, 0, zPos); 
  }
}
