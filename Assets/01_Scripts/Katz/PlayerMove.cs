using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float _moveSpeed;
    public float _rollSpeed;

    Animator _animator;
    SpriteRenderer _sprite;
    Rigidbody2D _rigid;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        //Flip
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            _animator.SetInteger("move", (int)Input.GetAxisRaw("Horizontal") * 2); //MoveAniamtor
            _sprite.flipX = Input.GetAxisRaw("Horizontal") > 0; //반전

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
            {
                _rigid.velocity = new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal"), 0); //움직임
            }
        }
        else
        {
            Vector3 _vector = Camera.main.WorldToScreenPoint(transform.position);

            _sprite.flipX = Input.mousePosition.x > _vector.x;
            _animator.SetInteger("move", _sprite.flipX ? 1 : -1);
        }
        Debug.Log(Input.GetAxisRaw("Roll"));
    }

    private void Update()
    {
        //람머스
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Roll");
            _rigid.AddForce(new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal") * _rollSpeed, 0), ForceMode2D.Impulse);
        }
    }
}
