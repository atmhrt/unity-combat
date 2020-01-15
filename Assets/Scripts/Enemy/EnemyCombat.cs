using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;

    private bool isBlocking = false;
    private float blockTimer = 2f;

    private EnemyMovement movementController;

    void Start()
    {
        movementController = gameObject.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (!isBlocking) {
            blockTimer -= Time.deltaTime;

            if (blockTimer <= 0.0f)
            {
                blockTimer = 2f;
                animator.SetTrigger("Block");
            }
        }

        if (Input.GetKeyDown("p"))
        {
            gameObject.transform.Find("Sparks").gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    public void GetAttacked(int damage)
    {
        health -= damage;

        if (health > 0) {
            animator.SetTrigger("HurtIdle");
        } else {
            animator.SetTrigger("Death");
            alive = false;
        }
    }

    public void SetIsBlocking()
    {
        isBlocking = true;
        Debug.Log("isBlocking");
    }

    public void UnsetIsBlocking()
    {
        isBlocking = false;
        Debug.Log("not isBlocking");
    }
}
