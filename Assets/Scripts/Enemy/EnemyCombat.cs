using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;

    public bool inAttackRange = false;

    public float blockTime = .6f;
    private float blockTimeLeft;

    private EnemyMovement movementController;

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
                !animator.GetBool("InPrimaryCombatAnim")
            ) {
                animator.SetTrigger("Block");
            }
            blockTimeLeft = blockTime;
        }
    }

    public void GetAttacked(int damage)
    {
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
