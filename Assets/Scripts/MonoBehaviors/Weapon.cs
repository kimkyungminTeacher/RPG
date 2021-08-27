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

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
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
}
