using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public Animator animator; // Referensi ke Animator

    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public bool isAbleToMove = true;

    private void Update()
    {
        if (isAbleToMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            // Periksa jika ada input untuk menentukan status berjalan
            bool isWalking = direction.magnitude >= 0.1f;
            animator.SetBool("IsWalking", isWalking); // Ubah nilai parameter di Animator

            if (isWalking)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * (speed * Time.deltaTime));
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
