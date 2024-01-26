using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;

    float currentHitPoint;

    void Awake() {
        currentHitPoint = maxHitPoints;
    }

    public void TakeDamage(float damageTaken) {
        currentHitPoint -= damageTaken;

        if (currentHitPoint < 1) {
            Destroy(gameObject);
        }
    }
}
