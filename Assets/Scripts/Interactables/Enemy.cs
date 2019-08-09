using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        //attack the enemy
        CharacterCombat playerCompat = PlayerManager.Instance.GetComponent<CharacterCombat>();
        if (playerCompat != null)
        {
            playerCompat.Attack(myStats);
        }
    }
}
