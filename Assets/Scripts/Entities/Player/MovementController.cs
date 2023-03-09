using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    private PlayerInputController _playerInputController;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    
    private static readonly int Running = Animator.StringToHash("Running");

    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = _playerInputController.moveValue * _runData.GetSpeed();

        _animator.SetBool(Running, _playerInputController.moveValue != Vector2.zero);
    }
}
