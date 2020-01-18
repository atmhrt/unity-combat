using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public bool alive = true;
    public int attackDamage = 20;
    public bool gotHurtInCurrAttack = false;

    // public LayerMask enemyLayers;

    private Collider2D swordCollider;
    private List<GameObject> hitEnemies = new List<GameObject>();

    public AudioSource[] sounds;

    void Start()
    {
        swordCollider = gameObject.transform.Find("SwordCollider").gameObject.GetComponent<Collider2D>();
        sounds = GetComponents<AudioSource>();
    }

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
            if (swordCollider.enabled)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies) {
                    Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
                    if (
                        swordCollider.IsTouching(enemyCollider)
                        && !hitEnemies.Contains(enemy)
                    )
                    {
                        enemy.GetComponent<EnemyCombat>().GetAttacked(attackDamage, gameObject.transform);
                        enemy.GetComponent<EnemyCombat>().gotHurtInCurrAttack = true;
                        hitEnemies.Add(enemy);
                    }
                }
            }

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
        sounds[1].PlayDelayed(.35f);

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //
        // foreach (Collider2D enemy in hitEnemies) {
        //     enemy.GetComponent<EnemyCombat>().GetAttacked(attackDamage);
        // }
    }

    void Block()
    {
        animator.SetTrigger("Block");
    }

    public void GetAttacked(int damage, Transform adversary)
    {
        if (animator.GetBool("IsBlocking"))
        {
            gameObject.transform.Find("Sparks").gameObject.GetComponent<ParticleSystem>().Play();
            sounds[0].Play(0);
        } else {
            health -= damage;

            if (health > 0) {
                animator.SetTrigger("HurtIdle");
                sounds[2].Play(0);
            } else {
                animator.SetTrigger("Death");
                alive = false;
            }
        }
    }

    public void ClearHitEnemies()
    {
        foreach (GameObject enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().gotHurtInCurrAttack = false;
        }

        hitEnemies.Clear();
    }
}
