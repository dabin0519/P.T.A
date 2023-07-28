using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cabenet : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTrm;
    [SerializeField] private float _checkDistance = 1f;

    public UnityEvent OnInCebent;
    public UnityEvent OnOutCebent;
    
    private SpriteRenderer _interactableSprite;
    private bool _isIn;

    private void Awake()
    {
        _interactableSprite = transform.Find("InterctableSprite").GetComponent<SpriteRenderer>();
        _interactableSprite.enabled = false;
    }

    private void Update()
    {
        if (Vector2.Distance(_playerVisualTrm.position, transform.position) < _checkDistance)
        {
            _interactableSprite.enabled = true;
            if (Input.GetKeyDown(KeyCode.F) && !_isIn)
            {
                OnInCebent?.Invoke();
                _isIn = true;
            }
            else if (Input.GetKeyDown(KeyCode.F) && _isIn)
            {
                OnOutCebent?.Invoke();
                _isIn = false;
            }
        }
        else
        {
            _interactableSprite.enabled = false;
        }
    }
}
