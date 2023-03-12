using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _cloudGenerator;

    // Start is called before the first frame update
    void Start()
    {
        var player = Instantiate(_runData.ClassType, _playerSpawnPoint.position, Quaternion.identity);
        _cinemachineVirtualCamera.Follow = player.transform;
        
        _cursor.GetComponent<CursorController>().SetWeaponsController(player.GetComponent<WeaponsController>());
        
        _runData.OnSetupRoom(player);
        _runData.CloudGenerator = _cloudGenerator.GetComponent<CloudGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        _runData.CompileStats();
    }
}
