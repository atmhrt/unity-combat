using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public Animator animator;
    public bool isFollowing = true;

    void Update()
    {
        LookAtTarget();

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > 4f) {
            animator.SetBool("IsWalking", true);
            FollowTarget(distance);
            isFollowing = true;
        } else {
            animator.SetBool("IsWalking", false);
            isFollowing = false;
        }
    }

    void LookAtTarget()
    {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FollowTarget(float distance)
    {
        Vector3 heading = target.position - transform.position;
        Vector3 direction = heading / distance;
        float deltaSpeed = moveSpeed * Time.deltaTime;

        transform.Translate(direction.x * deltaSpeed, direction.y * deltaSpeed, 0, Space.World);
    }

    public void FollowTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
