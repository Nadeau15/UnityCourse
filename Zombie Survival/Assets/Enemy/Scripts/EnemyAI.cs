using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  [SerializeField] float chaseRange = 5f;
  [SerializeField] float rotationSpeed = 5f;

  GameObject target;

  NavMeshAgent navMeshAgent;
  Animator enemyAnimator;
  float distanceToTarget = Mathf.Infinity;
  bool isProvoked = false;

  void Start()
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    enemyAnimator = GetComponent<Animator>();
    target = GameObject.FindGameObjectWithTag("Player");
  }

 void Update()
  {
    distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
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
    FaceTarget();
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
    navMeshAgent.SetDestination(target.transform.position);
  }

  void AttackTarget() {
    enemyAnimator.SetBool("attack", true);
  }

  void FaceTarget() {
    Vector3 direction = (target.transform.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
  }

  public void OnDamageTaken() {
    isProvoked = true;
  }
}
