using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;

    private Animator _animator;
    private Player _player;
    private Rigidbody2D _rigid;

    private Vector3 _vector;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player.GetState() != PlayerState.Move && _player.GetState() != PlayerState.Roll)
            return;

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            _animator.SetInteger("move", (int)Input.GetAxisRaw("Horizontal") * 2); //MoveAniamtor

            if (Input.GetAxisRaw("Horizontal") > 0) {
                transform.eulerAngles = new Vector3(0, 180);
            }

            else if(Input.GetAxisRaw("Horizontal") < 0) {
                transform.eulerAngles = new Vector3(0, 0);
            }

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
            {
                _rigid.velocity = new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal"), 0); //움직임
            }

            _player.SetState(PlayerState.Move);
        }
        else
        {
            _vector = Camera.main.WorldToScreenPoint(transform.position);

            if (Input.mousePosition.x > _vector.x) {
                transform.eulerAngles = new Vector3(0, 180);
            }

            else if (Input.mousePosition.x < _vector.x) {
                transform.eulerAngles = new Vector3(0, 0);
            }
            _animator.SetInteger("move", MoveDir() ? 1 : -1);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
        {
            _player.SetState(PlayerState.Roll);
            _animator.SetTrigger("Roll");
            _rigid.AddForce(new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal") * _rollSpeed, 0), ForceMode2D.Impulse);
        }

        if(_player.GetState() != PlayerState.Move)
        {
            if(Input.mousePosition.x > _vector.x)
            {
                transform.eulerAngles = new Vector3(0, 0);
            }
        }
    }

    private bool MoveDir() {
        if (Input.mousePosition.x > _vector.x) {
            return true;
        }

        else if (Input.mousePosition.x < _vector.x) {
            return false;
        }
        return false;
    }
}
