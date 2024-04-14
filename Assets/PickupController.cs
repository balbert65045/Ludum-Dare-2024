using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupController : MonoBehaviour
{
    [SerializeField] GameObject HoldingObjectPosition;
    bool PreparingToThrow = false;
    float timeOfPreparitionOfThrow;

    [SerializeField] float MaxChargeDistance = 1200f;
    [SerializeField] float MaxThrowVel = 10f;
    
    [SerializeField] LineRenderer lineRenderer;
    public int resolution = 30; // Number of points on the trajectory path
    float angle = 5f;

    bool throwHeldObj = false;
    float throwVel;

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
                    objectHolding.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    objectHolding.SetPickedUp(true);
                    objectHolding.GetComponent<MoveableCreature>().SetCapableOfMoving(false);
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

        if (PreparingToThrow)
        {
            Vector3 differenceInMousePos = mousPosDown - Input.mousePosition;
            dirToThrow = new Vector3(-differenceInMousePos.x, 0, -differenceInMousePos.y).normalized;
            throwVel = (Mathf.Clamp(differenceInMousePos.magnitude, 0, MaxChargeDistance) / MaxChargeDistance) * MaxThrowVel;
            Debug.Log(throwVel);
            RenderArc();
            if (Input.GetMouseButtonUp(0)){
                PreparingToThrow = false;
                throwHeldObj = true;
            }
        }

    }

    void RenderArc()
    {
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        float radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (throwVel * throwVel * Mathf.Sin(2 * radianAngle)) / Physics.gravity.magnitude;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float radianAngle = Mathf.Deg2Rad * angle;
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((Physics.gravity.magnitude * x * x) / (2 * throwVel * throwVel * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        Vector3 newPos = HoldingObjectPosition.transform.position + (dirToThrow * x) + new Vector3(0, y, 0);
        //return new Vector3(x, y);
        return newPos;
    }



    private void FixedUpdate()
    {
        if (objectHolding != null)
        {
            objectHolding.transform.position = HoldingObjectPosition.transform.position;
        }

        if (throwHeldObj)
        {
            lineRenderer.positionCount = 0;
            throwHeldObj = false;
            Rigidbody objectToThrow = objectHolding.GetComponent<Rigidbody>();
            objectToThrow.useGravity = true;
            objectToThrow.velocity = dirToThrow * throwVel;
            //objectToThrow.AddForce(dirToThrow * throwForce * objectToThrow.mass, ForceMode.Impulse);
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

    public void RemoveFromCapablePickups(PickUpableObj[] pickUpableObjs)
    {
        foreach(PickUpableObj obj in pickUpableObjs)
        {
            if (objsCapableOfPickingUp.Contains(obj)) { objsCapableOfPickingUp.Remove(obj); }
        }
    }
}
