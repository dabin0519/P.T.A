using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private float _checkDistance = 1f;
    //[SerializeField] private DialogueSO[] _dialogueSOs;

    public UnityEvent OnInteractable;
    
    private SpriteRenderer _interactableSprite;

    private void Awake()
    {
        _interactableSprite = transform.Find("InterctableSprite").GetComponent<SpriteRenderer>();
        _interactableSprite.enabled = false;
    }

    private void Update()
    {
        if(Vector2.Distance(_playerTrm.position, transform.position) < _checkDistance)
        {
            _interactableSprite.enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnInteractable?.Invoke();
            }
        }
        else
        {
            _interactableSprite.enabled = false;
        }
    }
}
