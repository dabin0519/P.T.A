using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position = new Vector2(h, v) * _moveSpeed * Time.deltaTime;

        _anim.SetFloat("MoveX", h);
        _anim.SetFloat("MoveY", v);
    }
}
