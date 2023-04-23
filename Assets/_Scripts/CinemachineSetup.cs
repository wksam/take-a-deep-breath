using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSetup : MonoBehaviour
{
    CinemachineBrain _cinemachineBrain;
    CinemachineVirtualCamera _cinemachineVirtualCamera;
    bool _isCinemachineSetupCompleted = false;

    void Awake()
    {
        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Start()
    {
        SetupCinemachine();
    }

    void Update()
    {
        if (!_isCinemachineSetupCompleted) SetupCinemachine();
    }

    void SetupCinemachine()
    {
        if (_cinemachineBrain == null) return;
        _cinemachineVirtualCamera = (CinemachineVirtualCamera)_cinemachineBrain.ActiveVirtualCamera;
        if (_cinemachineVirtualCamera == null) return;
        _cinemachineVirtualCamera.Follow = gameObject.transform;
        _isCinemachineSetupCompleted = true;
    }
}
