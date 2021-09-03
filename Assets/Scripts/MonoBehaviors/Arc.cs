using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        Vector3 startPosition = transform.position;
        float percentComplete = 0.0f;

        while (percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete);

            Debug.Log("TravelArc percentComplete : " + percentComplete + 
                    ", transform.position.x : " + transform.position.x);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
