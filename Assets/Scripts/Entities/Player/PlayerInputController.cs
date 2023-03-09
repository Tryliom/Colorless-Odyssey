using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 moveValue;
    public bool attackValue;
    public bool specialAttackValue;
    public bool dashValue;
    
    public void OnPlayerMove(InputValue context)
    {
        moveValue = context.Get<Vector2>();
    }

    public void OnPlayerAttack(InputValue context)
    {
        attackValue = context.isPressed;
    }
    
    public void OnPlayerSpecialAttack(InputValue context)
    {
        specialAttackValue = context.isPressed;
    }
    
    public void OnPlayerDash(InputValue context)
    {
        dashValue = context.isPressed;
    }
}