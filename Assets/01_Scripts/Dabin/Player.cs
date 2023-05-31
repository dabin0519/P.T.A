using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnum
{
    Roll,
    Attack,
    Parry,
    Move,
    Die
}

public class Player : MonoBehaviour
{
    public StateEnum State;
}
