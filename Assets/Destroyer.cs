using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickUpableObj>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
