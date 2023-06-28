using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Roll,
    Attack,
    Parry,
    Move,
    Grab,
    Assassin,
    Counter,
    Die,
    End
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerState _state;

    private void Awake()
    {
        _state = PlayerState.Move;
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }

    public PlayerState GetState()
    {
        return _state;
    }
}
