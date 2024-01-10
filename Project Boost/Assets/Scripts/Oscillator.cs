using System;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  Vector3 startingPosition;
  [SerializeField] Vector3 movementVector;
  [SerializeField] [Range(0,1)] float movementFactor;
  [SerializeField] float period = 2f;

  void Start()
  {
    startingPosition = transform.position;
  }

  void Update()
  {
    // We try not to compare a float value with 0. Do the follwing comparaison instead
    if (period > Mathf.Epsilon) {
      float cycles = Time.time / period;

      const float tau = Mathf.PI * 2;
      // rawSinWave is a value oscillating between -1 and 1
      float rawSinWave = Mathf.Sin(cycles * tau);

      // I want movementFactor to have a range between 0 and 1.
      movementFactor = (rawSinWave + 1f) / 2f;

      Vector3 offset = movementVector * movementFactor;
      transform.position = startingPosition + offset;
    }
  }
}
