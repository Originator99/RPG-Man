using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookradius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    private CharacterCombat combat;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
	}
	
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookradius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                //Attack Target
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats!=null)
                    combat.Attack(targetStats);
                //Face Target
                FaceTarget();
            }
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookradius);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
