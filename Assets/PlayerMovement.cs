using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float DashSpeed = 6f;
    [SerializeField] float DashTime = 1f;
    [SerializeField] float DashCooldown = 3f;
    private Rigidbody rb;        
    private Vector3 moveDirection = Vector3.zero;

    bool dashCoolingDown = false;
    bool dashAvailable = true;
    bool isDashing = false;
    float timeOfDash;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
            animator.SetBool("IsMoving", true);

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAvailable && !isDashing)
        {
            dashAvailable = false;
            isDashing = true;
            timeOfDash = Time.time;
        }

        if(dashCoolingDown && Time.time > timeOfDash + DashTime + DashCooldown)
        {
            dashCoolingDown = false;
            dashAvailable = true;
        }
    }

    void FixedUpdate()
    {
        float speed = MoveSpeed;
        if (isDashing)
        {
            speed = DashSpeed;
            if (Time.time > timeOfDash + DashTime)
            {
                isDashing = false;
                dashCoolingDown = true;
            }
        }
        animator.speed = speed * .1f;
        Vector3 vel = moveDirection * speed * 60f * Time.fixedDeltaTime;
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        //rb.velocity = moveDirection * speed * 60f * Time.fixedDeltaTime;
    }
}
