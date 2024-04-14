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
            FindObjectOfType<Score>().AdjustScore(pointsForTrash);
            StartCoroutine("ShowPoints");
        }
    }

    IEnumerator ShowPoints()
    {
        pointsText.gameObject.SetActive(true);
        pointsText.text = "+" + pointsForTrash.ToString();
        yield return new WaitForSeconds(1);
        pointsText.gameObject.SetActive(false);
    }
}
