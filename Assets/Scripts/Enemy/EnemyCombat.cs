using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;

    void Update()
    {

    }

    public void GetAttacked(int damage)
    {
        health -= damage;
        Debug.Log(health);

        if (health > 0) {
            animator.SetTrigger("HurtIdle");
        } else {
            animator.SetTrigger("Death");
            alive = false;
        }
    }
}
