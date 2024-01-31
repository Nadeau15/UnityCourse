using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] float chaseRange = 5f;

  NavMeshAgent navMeshAgent;
  Animator enemyAnimator;
  float distanceToTarget = Mathf.Infinity;
  bool isProvoked = false;

  void Start()
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    enemyAnimator = GetComponent<Animator>();
  }

 void Update()
  {
    distanceToTarget = Vector3.Distance(target.position, transform.position);
    if (isProvoked) {
      EngageTarget();
    } else if(distanceToTarget < chaseRange) {
      isProvoked = true;
    }
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, chaseRange);
  }

  void EngageTarget() {
    if(distanceToTarget >= navMeshAgent.stoppingDistance) {
      ChaseTarget();
    }

    if(distanceToTarget <= navMeshAgent.stoppingDistance) {
      AttackTarget();
    }
  }

  void ChaseTarget() {
    enemyAnimator.SetBool("attack", false);
    enemyAnimator.SetTrigger("move");
    navMeshAgent.SetDestination(target.position);
  }

  void AttackTarget() {
    enemyAnimator.SetBool("attack", true);
    Debug.Log(name + " is attacking " + target.name);
  }
}
