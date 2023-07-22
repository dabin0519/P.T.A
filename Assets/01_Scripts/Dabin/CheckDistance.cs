using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTrm;
    [SerializeField] private float _distance;
    public UnityEvent OnCheck;

    private void Update()
    {
        if(Vector2.Distance(transform.position, _playerVisualTrm.position) < _distance)
        {
            OnCheck?.Invoke();
        }
    }
}
