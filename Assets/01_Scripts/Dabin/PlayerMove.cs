using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;
    [HideInInspector] public SpriteRenderer SpriteRend;

    private Animator _anim;
    private Player _player;
    private Rigidbody2D _rigid;

    private Vector3 _vector;

    private void Awake() {
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player.GetState() == PlayerState.Die || 
            _player.GetState() != PlayerState.Move && _player.GetState() != PlayerState.Roll)
            return;

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            _anim.SetInteger("move", (int)Input.GetAxisRaw("Horizontal") * 2); //MoveAniamtor

            if (Input.GetAxisRaw("Horizontal") > 0) {
                transform.eulerAngles = new Vector3(0, 180);
            }

            else if(Input.GetAxisRaw("Horizontal") < 0) {
                transform.eulerAngles = new Vector3(0, 0);
            }

            if (!_anim.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
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
            _anim.SetInteger("move", MoveDir() ? 1 : -1);
        }

    }

    private void Update()
    {
        if (_player.GetState() == PlayerState.End)
            return;

        if(_player.GetState() == PlayerState.Die)
        {
            _anim.SetTrigger("isDie");
            _player.SetState(PlayerState.End);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
        {
            _player.SetState(PlayerState.Roll);
            _anim.SetTrigger("Roll");
            _rigid.AddForce(new Vector2(_moveSpeed * Input.GetAxisRaw("Horizontal") * _rollSpeed, 0), ForceMode2D.Impulse);
        }

        if(_player.GetState() != PlayerState.Move && _player.GetState() != PlayerState.Grab)
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
