using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {
    const float locoMotionAnimationSmoothTime = 0.1f;

    private NavMeshAgent agent;
    private Animator animator;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locoMotionAnimationSmoothTime, Time.deltaTime);
	}
}
