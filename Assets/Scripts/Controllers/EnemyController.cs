using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float lookRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    private CharacterCombat combat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.Instance.Player.transform;
        combat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //attack the target
                combat.Attack(target.GetComponent<CharacterStats>());

                //face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
