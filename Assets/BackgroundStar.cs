using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundStar : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        MoveSpeed = MoveSpeed * Random.Range(.75f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = Vector3.Lerp(transform.position + Vector3.left, transform.position, MoveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + Vector3.left * MoveSpeed * Time.fixedDeltaTime);
    }
}
