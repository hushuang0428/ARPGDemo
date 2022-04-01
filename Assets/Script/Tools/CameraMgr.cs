using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : SingletonMono<CameraMgr>
{

    private CinemachineVirtualCamera playerCamera;

    private void Awake()
    {
        base.Awake(); 
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();

       
    }

    public void SetLookAndFllow(Transform target)
    {
        
        if (playerCamera != null)
        {
            playerCamera.Follow = target;
            playerCamera.LookAt = target;
        }

    }
}
