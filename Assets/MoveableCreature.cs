using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableCreature : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float TimeUntilNextDirectionToMovetoMin = 1f;
    [SerializeField] float TimeUntilNextDirectionToMovetoMax = 5f;
    float TimeUntilNextDirectionToMoveto;

    [SerializeField] float rotationSpeed = 100f;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private PickUpableObj pickUpableObj;
    Animator animator;

    float DelayToMoveFromStart = 3f;
    float timeToBeAbleToCheck;
    public bool CapableOfMoving = true;
    public void SetCapableOfMoving(bool value) { 
        CapableOfMoving=value;
        animator.SetBool("IsWalking", value);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        pickUpableObj = GetComponent<PickUpableObj>();
        DiscoverNextMovePos();
        StartCoroutine("ConsiderNewDirection");
        rb.constraints = RigidbodyConstraints.None;
        SetCapableOfMoving(false);
        timeToBeAbleToCheck = Time.timeSinceLevelLoad + DelayToMoveFromStart;
    }

    IEnumerator ConsiderNewDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeUntilNextDirectionToMoveto);
            DiscoverNextMovePos();
        }
    }

    void DiscoverNextMovePos()
    {
        float xDir = Random.Range(-1f, 1f);
        float zDir = Random.Range(-1f, 1f);
        TimeUntilNextDirectionToMoveto = Random.Range(TimeUntilNextDirectionToMovetoMin, TimeUntilNextDirectionToMovetoMax);
        moveDirection = new Vector3(xDir, 0.0f, zDir).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < timeToBeAbleToCheck) { return; }
        if (CapableOfMoving && !pickUpableObj.isPickedUp && pickUpableObj.zoneIn == null)
        {
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        if(!CapableOfMoving && !pickUpableObj.isPickedUp && pickUpableObj.zoneIn == null)
        {
            if(rb.velocity.magnitude < 1f)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                SetCapableOfMoving(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (CapableOfMoving && !pickUpableObj.isPickedUp && pickUpableObj.zoneIn == null)
        {
            //rb.velocity = (moveDirection * moveSpeed * 60f * Time.fixedDeltaTime);
            Vector3 vel = (moveDirection * moveSpeed * 60f * Time.fixedDeltaTime);
            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Wall>() != null)
        {
            moveDirection = -moveDirection;
        }
    }
}
