using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;

    private EnemyMovement movementController;

    void Start()
    {
        movementController = gameObject.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        Debug.Log(movementController.isFollowing);
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
}
