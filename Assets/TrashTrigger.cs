using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashTrigger : MonoBehaviour
{
    [SerializeField] int pointsForTrash = 1;
    [SerializeField] TMP_Text pointsText;
    PickUpableObj lastObjectThrough;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickUpableObj>() != null && other.GetComponent<PickUpableObj>() != lastObjectThrough)
        {
            lastObjectThrough = other.GetComponent<PickUpableObj>();
            GetComponent<AudioSource>().Play();
            int value = FindObjectOfType<Score>().multiplierValue * pointsForTrash;
            FindObjectOfType<Score>().AdjustScore(value);
            StartCoroutine("ShowPoints", value);
        }
    }

    IEnumerator ShowPoints(int value)
    {
        pointsText.gameObject.SetActive(true);
        pointsText.text = "+" + value.ToString();
        yield return new WaitForSeconds(1);
        pointsText.gameObject.SetActive(false);
    }
}
