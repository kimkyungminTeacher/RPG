using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;

    private void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }

    public GameObject SpawnObject()
    {
        if (prefabToSpawn == null)
        {
            Debug.Log("prefabToSpawn is null");
            return null;
        }

        return Instantiate(prefabToSpawn,
            transform.position, Quaternion.identity);
    }
}
