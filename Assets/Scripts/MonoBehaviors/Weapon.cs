using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    public int poolSize = 7;
    static List<GameObject> ammoPool = new List<GameObject>();
    public float weaponVelocity = 2;
    Camera localCamera;

    float positiveSlope;
    float negativeSlope;

    bool isFiring;
    [HideInInspector]
    public Animator animator;

    enum Quadrant
    {
        East, 
        South, 
        West,
        North
    }

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    private void Start()
    {
        localCamera = Camera.main;
        isFiring = false;
        animator = GetComponent<Animator>();
        
        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    bool HighterThanPositiveSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);

        //b = y - mx
        float playerIntercept 
            = playerPosition.y - (positiveSlope * playerPosition.x);
        float mouseIntercept
            = mousePosition.y - (positiveSlope * mousePosition.x);

        return playerIntercept < mouseIntercept;
    }

    bool HigherThanNegativeSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPostion = gameObject.transform.position;
        Vector2 mousePosition 
            = localCamera.ScreenToWorldPoint(inputPosition);

        //b = y - mx
        float playerIntercept
            = playerPostion.y - (negativeSlope * playerPostion.x);
        float mouseIntercept
            = mousePosition.y - (negativeSlope * mousePosition.y);

        return playerIntercept < mouseIntercept;
    }

    Quadrant GetQuadrant()
    {
        bool highterThanPos
            = HighterThanPositiveSlopeLine(Input.mousePosition);
        bool higherThanNega
            = HigherThanNegativeSlopeLine(Input.mousePosition);

        if (highterThanPos == false && higherThanNega == true)
        {
            return Quadrant.East;
        }
        else if (highterThanPos == false && higherThanNega == false)
        {
            return Quadrant.South;
        }
        else if (highterThanPos == true && higherThanNega == false)
        {
            return Quadrant.West;
        }
        else
        {
            return Quadrant.North;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            FireAmmo();
        }

        UpdateState();
    }

    private void UpdateState()
    {
        if (isFiring)
        {
            Vector2 qVector;
            Quadrant quadrant = GetQuadrant();
            switch(quadrant)
            {
                case Quadrant.East:
                    qVector = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrant.South:
                    qVector = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrant.West:
                    qVector = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrant.North:
                    qVector = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    qVector = new Vector2(0.0f, 0.0f);
                    break;
            }

            animator.SetBool("isFiring", true);
            animator.SetFloat("fireXDir", qVector.x);
            animator.SetFloat("fireYDir", qVector.y);

            isFiring = false;
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    private void FireAmmo()
    {
        GameObject ammo = SpawnAmmo(transform.position);
        if (ammo == null)
            return;

        Arc arcScript = ammo.GetComponent<Arc>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float travelDuration = 1.0f / weaponVelocity;
        StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }

    float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);
    }

}
