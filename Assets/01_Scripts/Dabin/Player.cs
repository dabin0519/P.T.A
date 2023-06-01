using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Roll,
    Attack,
    Parry,
    Move,
    Die
}

public class Player : MonoBehaviour
{
    private PlayerState _state;

    private void Awake()
    {
        _state = PlayerState.Move;
    }

    public void ChangeState(PlayerState state)
    {
        _state = state;
    }

    public PlayerState GetState()
    {
        return _state;
    }
}
