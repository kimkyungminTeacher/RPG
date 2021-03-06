using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;
    public SpawnPoint playerSpawnPoint;
    public RPGCameraManager cameraManager;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerSpawnPoint == null)
        {
            Debug.LogError("SpawnPlayer is null");
            return;
        }
        GameObject player = playerSpawnPoint.SpawnObject();
        cameraManager.virtualCamera.Follow = player.transform;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
