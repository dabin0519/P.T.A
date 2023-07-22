using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    [SerializeField] private Animator _playerSleepAnim;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _backgrounds;

    private void Awake()
    {
        _playerSleepAnim.enabled = false;
    }

    public override void CallStage()
    {
        _playerSleepAnim.enabled = true;
        _player.SetActive(false);
    }

    public override void EndDialogue()
    {
        _player.SetActive(true);
        _playerSleepAnim.gameObject.SetActive(false);
        _backgrounds[0].SetActive(false);
        _backgrounds[1].SetActive(true);
    }
}
