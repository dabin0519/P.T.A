using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cabenet : MonoBehaviour
{
    private bool _isIn;

    public UnityEvent Out;

    public void In()
    {
        _isIn = true;
    }

    private void Update()
    {
        if (_isIn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Out?.Invoke();
                _isIn = false;
            }
        }
    }
}
