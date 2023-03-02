using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        var player = Instantiate(_runData.classType.gameObject, _playerSpawnPoint.position, Quaternion.identity);
        _cinemachineVirtualCamera.Follow = player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
