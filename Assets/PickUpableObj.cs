using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObj : MonoBehaviour
{
    public AudioSource BeingThrown;
    public RequestTypes myRequestType;
    public SummoningZone zoneIn = null;
    public bool isPickedUp = false;
    public void SetPickedUp(bool value) {  isPickedUp = value; }
}
