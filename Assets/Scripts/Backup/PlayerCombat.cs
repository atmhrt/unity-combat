// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class PlayerCombat : MonoBehaviour
// {
//     public Animator animator;
//     public float attackSpeed = 2f;
//     float nextAttackTime = 0f;
//
//     public Transform attackPoint;
//     public float attackRange = 0.5f;
//     public LayerMask enemyLayers;
//
//     public int attackDmg = 20;
//
//     void Update()
//     {
//         if (Time.time >= nextAttackTime)
//         {
//             if (Input.GetMouseButtonDown(0))
//             {
//                 Attack();
//                 nextAttackTime = Time.time + 1 / attackSpeed;
//             }
//         }
//     }
//
//     void Attack()
//     {
//         animator.SetTrigger("Attack");
//
//         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
//
//         foreach (Collider2D enemy in hitEnemies)
//         {
//             enemy.GetComponent<Enemy>().TakeDamage(attackDmg);
//         }
//     }
//
//     void OnDrawGizmosSelected()
//     {
//         if (attackPoint == null) return;
//         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//     }
// }
//
// // Input.GetMouseButtonDown();
// // Input.mousePosition;
// // Camera.main.ScreenToWorldPoint(mousePos) — convert pixel position to world units
