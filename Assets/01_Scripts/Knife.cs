using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    
    public void SetBool()
    {
        _playerAnim.SetBool("Weapon", true);
    }
}
