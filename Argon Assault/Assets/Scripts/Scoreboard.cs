using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    int score = 0;
    TMP_Text scoreText;

    void Start() {
      scoreText = GetComponent<TMP_Text>();
      scoreText.text = score.ToString();
    }
    
    public void IncreaseScore(int newPoints) {
      score += newPoints;
      scoreText.text = score.ToString();
    }
}
