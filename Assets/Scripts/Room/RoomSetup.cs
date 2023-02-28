using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    [Header("ClassTypes")]
    [SerializeField] private GameObject _mage;
    [SerializeField] private GameObject _slime;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_runData.classType == null)
        {
            _runData.classType = _mage;
        }
        
        var player = Instantiate(_runData.classType.gameObject, _playerSpawnPoint.position, Quaternion.identity);
        _cinemachineVirtualCamera.Follow = player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
