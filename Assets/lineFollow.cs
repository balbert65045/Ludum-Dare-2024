using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineFollow : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3[] lastPositions = new Vector3[10];
    [SerializeField] PickUpableObj obj;
    public bool LineActive = false;
    float timeOfLineDrawn = .5f;
    float offset = -0.1f;
    float timeOfShot;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetPositionsToOrigin();
        lineRenderer.positionCount = 0;

    }

    void UpdateLastPositions()
    {
        for (int i = 9; i > 0; i--)
        {
            lastPositions[i] = lastPositions[i - 1];
        }
        lastPositions[0] = obj.transform.position - (Vector3.up * offset);
    }

    void UpdateLine()
    {
        for (int i = 0; i < lastPositions.Length; i++)
        {
            lineRenderer.SetPosition(i, lastPositions[i]);
        }
    }

    void SetPositionsToOrigin()
    {
        for (int i = 0; i < lastPositions.Length; i++)
        {
            lastPositions[i] = obj.transform.position - (Vector3.up * offset);
        }
    }

    public void Throw()
    {
        lineRenderer.positionCount = 10;
        SetPositionsToOrigin();
        lineRenderer.enabled = true;
        timeOfShot = Time.time;
        StartCoroutine("ThrowAndDraw");
        LineActive = true;
    }

    IEnumerator ThrowAndDraw()
    {
        while (timeOfLineDrawn + timeOfShot > Time.time)
        {
            UpdateLastPositions();
            UpdateLine();
            yield return new WaitForFixedUpdate();
        }
        LineActive = false;
        SetPositionsToOrigin();
        lineRenderer.enabled = false;
    }
}
