using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField] StartZone start;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<BackgroundStar>() != null)
        {
            other.transform.position = new Vector3(start.transform.position.x, other.transform.position.y, other.transform.position.z);
        }
    }
}
