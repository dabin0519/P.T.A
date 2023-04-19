using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;

    private Animator _animator;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigid;

    public bool IsParry;
    private Vector3 _vector;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsParry)
        {
            _sprite.flipX = Input.mousePosition.x > _vector.x ? false : false;
            return;
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
            _animator.SetTrigger("Roll");
            _rigid.AddForce(new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal") * _rollSpeed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(1) && !IsParry)
        {
            IsParry = true;
            _animator.SetTrigger("Parry");
        }
    }

    public void FinishParry()
    {
        IsParry = false;
    }
}
