using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Player
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;

    private Animator _animator;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigid;

    private Vector3 _vector;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(State != StateEnum.Move)
        {

        }

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
            _vector = Camera.main.WorldToScreenPoint(transform.position);

            _sprite.flipX = Input.mousePosition.x > _vector.x;
            _animator.SetInteger("move", _sprite.flipX ? 1 : -1);
        }
    }

    private void Update()
    {
        //람머스
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
        {
            State = StateEnum.Roll;
            _animator.SetTrigger("Roll");
            _rigid.AddForce(new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal") * _rollSpeed, 0), ForceMode2D.Impulse);
        }
    }
}
