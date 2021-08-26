using System.Collections;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    //float currentAngle;

    public float directionChangeInterval;

    Vector3 endPosition;
    Transform targetTransform = null;
    Coroutine moveCoroutine;

    CircleCollider2D circleCollider;
    Rigidbody2D rb2d;
    Animator animator;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        Debug.DrawLine(transform.position, endPosition, Color.red);
    }

    private IEnumerator wanderRoutine()
    {
        while (true)
        {
            endPosition = ChooseNewEndposition();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move());
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
        }
    }

    private Vector3 ChooseNewEndposition()
    {
        float currentAngle = Random.Range(0, 360);
        float inputAngleRadians = currentAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentSpeed = pursuitSpeed;
            animator.SetBool("isWalking", true);

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move());
            targetTransform = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentSpeed = wanderSpeed;
            animator.SetBool("isWalking", false);

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }
}
