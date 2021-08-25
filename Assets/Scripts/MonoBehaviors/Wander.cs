using System.Collections;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    float currentAngle;

    public float directionChangeInterval;

    Vector3 endPosition;
    Transform targetTransform = null;
    Coroutine moveCoroutine;

    CircleCollider2D circleCollider;
    Rigidbody2D rb2d;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        currentSpeed = wanderSpeed;
        StartCoroutine(wanderRoutine());
    }

    private void OnDrawGizmos()
    {
        if (circleCollider == null)
            return;

        Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
    }

    private void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }

    private IEnumerator wanderRoutine()
    {
        while (true)
        {
            //«“¿œ
            endPosition = ChooseNewEndposition();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(move());
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private IEnumerator move()
    {
        Debug.Log("move....... start");
        while (true)
        {
            //Debug.Log("move");
            yield return new WaitForFixedUpdate();
        }
    }

    private Vector3 ChooseNewEndposition()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        
        float inputAngleRadians = currentAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }
}
