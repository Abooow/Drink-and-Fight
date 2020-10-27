using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFocus : MonoBehaviour
{
    public static CameraTargetFocus Instance;

    public Transform Target 
    { 
        get => _target;
        set 
        {
            _target = value;
            virtualCamera.Follow = _target;
        }
    }
    [SerializeField] private Transform _target;

    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        Target = _target;
    }
}
