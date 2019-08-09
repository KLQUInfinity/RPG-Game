using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float AttackSpeed = 1f;

    public bool InCombat{ get; private set; }

    public event System.Action OnAttack;

    private float attackCooldown = 0f;
    private CharacterStats myStats;
    private const float combatCooldown = 5f;
    private float lastAttackTime;

    [SerializeField] private float attackDelay = 0.6f;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1 / AttackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.Damage.GetValue());
        if (stats.CurrentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
