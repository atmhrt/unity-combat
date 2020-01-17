using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;
    public int attackDamage = 10;

    public bool inAttackRange = false;

    public float blockTime = 1f;
    private float blockTimeLeft;

    public float minAttackTime = 1f;
    public float maxAttackTime = 2.5f;
    private float attackTimeLeft;

    public bool gotHurtInCurrAttack = false;

    public bool targetInAttackRange = false;

    public EnemyMovement movementController;

    private Collider2D swordCollider;

    private List<GameObject> hitEnemies = new List<GameObject>();

    void Start()
    {
        movementController = gameObject.GetComponent<EnemyMovement>();
        blockTimeLeft = blockTime;
        swordCollider = gameObject.transform.Find("SwordCollider").gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        blockTimeLeft -= Time.deltaTime;
        attackTimeLeft -= Time.deltaTime;

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

        if (
            // targetInAttackRange &&
            attackTimeLeft <= 0.0f)
        {
            if (
                !animator.GetBool("InPrimaryCombatAnim") &&
                !gotHurtInCurrAttack
            ) {
                animator.SetTrigger("Attack");
            }
            attackTimeLeft = Random.Range(minAttackTime, maxAttackTime);
        }

        if (
            animator.GetBool("WeaponDrawn")
        )
        {
            if (swordCollider.enabled)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject enemy in enemies) {
                    Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
                    if (
                        swordCollider.IsTouching(enemyCollider)
                        && !hitEnemies.Contains(enemy)
                    )
                    {
                        enemy.GetComponent<Combat>().GetAttacked(attackDamage, gameObject.transform);
                        enemy.GetComponent<Combat>().gotHurtInCurrAttack = true;
                        hitEnemies.Add(enemy);
                    }
                }
            }
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

    public void ClearHitEnemies()
    {
        foreach (GameObject enemy in hitEnemies)
        {
            enemy.GetComponent<Combat>().gotHurtInCurrAttack = false;
        }

        hitEnemies.Clear();
    }
}
