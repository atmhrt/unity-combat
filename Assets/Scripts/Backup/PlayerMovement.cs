// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     private Vector3 moveTarget;
//
//     private Rigidbody2D rb2d;
//     public Animator animator;
//
//
//     void Start()
//     {
//         rb2d = GetComponent<Rigidbody2D>();
//     }
//
//     void Update() {
//         if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
//         {
//             moveTarget = Input.mousePosition;
//
//             Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//             float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
//             transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//         }
//
//         float distance = Vector3.Distance(Camera.main.WorldToScreenPoint(transform.position), moveTarget);
//         // Debug.Log(moveTarget == default(Vector3));
//         if (moveTarget != default(Vector3) && distance > 1.0f)
//         {
//             Vector3 heading = moveTarget - Camera.main.WorldToScreenPoint(transform.position);  //use transform.position if you are using world space
//             Vector3 direction = heading / distance; // Normalizing
//
//             float deltaSpeed = moveSpeed * Time.deltaTime;
//             transform.Translate(direction.x * deltaSpeed, direction.y * deltaSpeed, 0, Space.World);
//         }
//
//         // if (Input.GetMouseButton(0)) {
//         //     Vector3 pos = Input.mousePosition;
//         //     pos.z = transform.position.z - Camera.main.transform.position.z;
//         //     pos = Camera.main.ScreenToWorldPoint(pos);
//         //     transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
//         // }
//     }
//
//     void FixedUpdate()
//     {
//         Move();
//
//         // if ()
//         FaceMouse();
//
//         //
//         // if (Input.GetMouseButtonDown(0))
//         // {
//         //     MoveTowardsPoint();
//         // }
//     }
//
//     void Move()
//     {
//         float horizontalInput = Input.GetAxis("Horizontal");
//         float verticalInput = Input.GetAxis("Vertical");
//
//         if (horizontalInput > 0f || verticalInput > 0f)
//         {
//             animator.SetBool("Walk", true);
//         } else
//         {
//             animator.SetBool("Walk", false);
//         }
//
//         transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, verticalInput * moveSpeed * Time.deltaTime, 0);
//         // rb2d.MovePosition(new Vector2((transform.position.x + horizontalInput * moveSpeed * Time.deltaTime), transform.position.y + verticalInput * moveSpeed * Time.deltaTime));
//
//     }
//
//     void MoveTowardsPoint()
//     {
//         float horizontalInput = Input.GetAxis("Vertical");
//         Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         targetPos.z = transform.position.z;
//         transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
//     }
//
//     void FaceMouse()
//     {
//         Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//         float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
//         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//     }
// }
