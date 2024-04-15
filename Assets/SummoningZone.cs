using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningZone : MonoBehaviour
{
    public AudioSource SuccessAudio;
    public AudioSource FailAudio;
    public AudioSource EnterAudio;

    [SerializeField] ParticleSystem particles;
    [SerializeField] Light myLight;

    public SpriteRenderer circle;
    public List<PickUpableObj> objsInCircle = new List<PickUpableObj>();
    [SerializeField] GameObject[] Spots;

    public Action OnChangedInsidePortal;
    public Action OnSummonFinished;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<PickUpableObj>() != null && !other.transform.GetComponent<PickUpableObj>().isPickedUp && other.transform.GetComponent<PickUpableObj>().zoneIn == null)
        {
            if (objsInCircle.Count >= Spots.Length) { return; }
            if (objsInCircle.Contains(other.transform.GetComponent<PickUpableObj>())) { return; }
            objsInCircle.Add(other.transform.GetComponent<PickUpableObj>());
            EnterAudio.Play();
            if (OnChangedInsidePortal != null) { OnChangedInsidePortal(); }
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().useGravity = false;
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<PickUpableObj>().GetComponent<Rigidbody>().AddTorque(Vector3.right);
            other.transform.GetComponent<PickUpableObj>().zoneIn = this;
            other.transform.GetComponent<MoveableCreature>().SetCapableOfMoving(false);
        }
    }

    public void RemoveObj(PickUpableObj Obj)
    {
        Obj.zoneIn = null;
        objsInCircle.Remove(Obj);
        if (OnChangedInsidePortal != null) { OnChangedInsidePortal(); }
    }

    public void SummonObjs()
    {
        StartCoroutine("TriggerTheSummoning");
    }

    
    IEnumerator TriggerTheSummoning()
    {
        while( myLight.intensity < 20)
        {
            myLight.intensity += 20 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        myLight.intensity = 0;
        particles.Play();
        GetRidOfTheObjects();
    }

    void GetRidOfTheObjects()
    {
        if (OnSummonFinished != null) { OnSummonFinished(); }
        PickUpableObj[] Objs = objsInCircle.ToArray();
        FindObjectOfType<PickupController>().RemoveFromCapablePickups(Objs);
        foreach (PickUpableObj Obj in Objs)
        {
           // RemoveObj(Obj);
            Destroy(Obj.gameObject);
        }
        objsInCircle.Clear();
        if (OnChangedInsidePortal != null) { OnChangedInsidePortal(); }
    }
    

    private void FixedUpdate()
    {
        for(int i = 0; i < objsInCircle.Count; i++)
        {
            objsInCircle[i].transform.position = Vector3.Lerp(objsInCircle[i].transform.position, Spots[i].transform.position, Time.deltaTime * 3f);
        }
    }
}
