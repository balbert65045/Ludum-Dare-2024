using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] GameObject HoldingObjectPosition;
    bool PreparingToThrow = false;
    float timeOfPreparitionOfThrow;

    [SerializeField] float MaxChargeDistance = 1200f;
    [SerializeField] float MaxThrowForce = 10f;

    bool throwHeldObj = false;
    float throwForce;

    Vector3 mousPosDown;
    Vector3 dirToThrow;

    List<PickUpableObj> objsCapableOfPickingUp = new List<PickUpableObj>();
    PickUpableObj objectHolding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            //PickUp
            if (objectHolding == null)
            {
                if (objsCapableOfPickingUp.Count != 0)
                {
                    objectHolding = objsCapableOfPickingUp[0];
                    objectHolding.GetComponent<Rigidbody>().useGravity = false;
                    objectHolding.SetPickedUp(true);
                    objsCapableOfPickingUp.Remove(objectHolding);
                    if (objectHolding.zoneIn)
                    {
                        objectHolding.zoneIn.RemoveObj(objectHolding);
                    }
                }
            }

            //Throw
            else
            {
                mousPosDown = Input.mousePosition;
                PreparingToThrow = true;
                timeOfPreparitionOfThrow = Time.time;
            }
        }

        if (PreparingToThrow && (Input.GetMouseButtonUp(0)))
        {
            Vector3 differenceInMousePos = mousPosDown - Input.mousePosition;
            dirToThrow = new Vector3(-differenceInMousePos.x, 0, -differenceInMousePos.y).normalized;
            PreparingToThrow = false;
            throwHeldObj = true;
            throwForce = (Mathf.Clamp(differenceInMousePos.magnitude, 0, MaxChargeDistance) / MaxChargeDistance) * MaxThrowForce;
        }

    }

    private void FixedUpdate()
    {
        if (objectHolding != null)
        {
            objectHolding.transform.position = HoldingObjectPosition.transform.position;
        }

        if (throwHeldObj)
        {
            throwHeldObj = false;
            Rigidbody objectToThrow = objectHolding.GetComponent<Rigidbody>();
            objectToThrow.useGravity = true;
            objectToThrow.AddForce(dirToThrow * throwForce, ForceMode.Impulse);
            objectHolding.SetPickedUp(false);
            objectHolding = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickUpableObj>() != null)
        {
            objsCapableOfPickingUp.Add(other.GetComponent<PickUpableObj>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PickUpableObj>() != null)
        {
            objsCapableOfPickingUp.Remove(other.GetComponent<PickUpableObj>());
        }
    }
}
