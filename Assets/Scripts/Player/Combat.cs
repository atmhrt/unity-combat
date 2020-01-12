using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 20;

    public LayerMask enemyLayers;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space");
            if (animator.GetBool("WeaponDrawn")) {
                animator.SetTrigger("HideWeapon");
                animator.SetBool("WeaponDrawn", false);
             } else {
                animator.SetTrigger("DrawWeapon");
                animator.SetBool("WeaponDrawn", true);
            }
        }

        if (animator.GetBool("WeaponDrawn"))
        {
            if (Input.GetMouseButtonDown(0)) {
                Attack();
            } else if (Input.GetMouseButtonDown(1)) {
                Block();
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyCombat>().GetAttacked(attackDamage);
        }
    }

    void Block()
    {
        animator.SetTrigger("Block");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
