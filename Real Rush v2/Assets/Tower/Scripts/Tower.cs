using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  [SerializeField] int cost = 75;
  [SerializeField] float builDelay = 1f;

  void Start() {
    StartCoroutine(Build());
  }

  public bool CreateTower(Tower tower, Vector3 position) {
    Bank bank = FindObjectOfType<Bank>();

    if(bank && bank.CurrentBalance >= cost) {
      bank.Withdraw(cost);
      Instantiate(tower.gameObject, position, Quaternion.identity);
      return true;
    }

    return false;
  }

  IEnumerator Build() {
    foreach(Transform child in transform) {
      child.gameObject.SetActive(false);
      foreach(Transform grandChild in child) {
        grandChild.gameObject.SetActive(false);
      }
    }

    foreach(Transform child in transform) {
      child.gameObject.SetActive(true);
      yield return new WaitForSeconds(builDelay);

      foreach(Transform grandChild in child) {
        grandChild.gameObject.SetActive(true);
      }
    }
  }
}
