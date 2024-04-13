using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningZone : MonoBehaviour
{
    public List<PickUpableObj> objsInCircle = new List<PickUpableObj>();
    [SerializeField] GameObject[] Spots;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<PickUpableObj>() != null && !other.transform.GetComponent<PickUpableObj>().isPickedUp && other.transform.GetComponent<PickUpableObj>().zoneIn == null)
        {
            if (objsInCircle.Count >= Spots.Length) { return; }
            objsInCircle.Add(other.transform.GetComponent<PickUpableObj>());
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().useGravity = false;
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().AddTorque(Vector3.right);
            other.transform.GetComponent<PickUpableObj>().zoneIn = this;
        }
    }

    public void RemoveObj(PickUpableObj Obj)
    {
        Obj.zoneIn = null;
        objsInCircle.Remove(Obj);
    }

    public void SummonObjs()
    {
        PickUpableObj[] Objs = objsInCircle.ToArray();
        foreach(PickUpableObj Obj in Objs) { 
            RemoveObj(Obj);
            Destroy(Obj.gameObject);
        }

    }

    private void FixedUpdate()
    {
        for(int i = 0; i < objsInCircle.Count; i++)
        {
            objsInCircle[i].transform.position = Vector3.Lerp(objsInCircle[i].transform.position, Spots[i].transform.position, Time.deltaTime * 3f);
        }
    }
}
