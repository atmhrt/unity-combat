using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;

    public bool inAttackRange = false;

    public float blockTime = 1f;
    private float blockTimeLeft;

    public bool gotHurtInCurrAttack = false;

    public EnemyMovement movementController;

    void Start()
    {
        movementController = gameObject.GetComponent<EnemyMovement>();
        blockTimeLeft = blockTime;
    }

    void Update()
    {
        blockTimeLeft -= Time.deltaTime;

        if (blockTimeLeft <= 0.0f)
        {
            if (
                inAttackRange &&
                !animator.GetBool("InPrimaryCombatAnim") &&
                !gotHurtInCurrAttack
            ) {
                animator.SetTrigger("Block");
            }
            blockTimeLeft = blockTime;
        }
    }

    public void GetAttacked(int damage, Transform adversary)
    {
        if (!animator.GetBool("WeaponDrawn"))
        {
            health -= damage;

            movementController.FollowTarget(adversary);
            animator.SetTrigger("DrawWeapon");
            animator.SetBool("WeaponDrawn", true);
        } else {
            if (animator.GetBool("IsBlocking"))
            {
                gameObject.transform.Find("Sparks").gameObject.GetComponent<ParticleSystem>().Play();
            } else {
                health -= damage;

                if (health > 0) {
                    animator.SetTrigger("HurtIdle");
                } else {
                    animator.SetTrigger("Death");
                    alive = false;
                }
            }
        }
    }
}
