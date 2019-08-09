using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] DefaultAttackAnimSet;

    private const float locmotionAnimSmoothTime = 0.1f;
    private NavMeshAgent agent;
    protected Animator myAnim;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;
    protected AnimationClip[] currentAttackAnimSet;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnim = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(myAnim.runtimeAnimatorController);
        myAnim.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = DefaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        myAnim.SetFloat("speedPercent", speedPercent, locmotionAnimSmoothTime, Time.deltaTime);
        myAnim.SetBool("inCompat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        myAnim.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
