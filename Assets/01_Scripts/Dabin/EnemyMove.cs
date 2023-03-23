using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("-------PlayLimit--------")]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;

    [Header("----------Move----------")]
    [SerializeField] float moveSpeed;
    [SerializeField] float StopTime;

    [Header("--------Collison---------")]
    [SerializeField] LayerMask raycastMask;
    [SerializeField] float rayCastLength;
    [SerializeField] float attackDistance;
    [SerializeField] float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    [SerializeField] public GameObject hotZone;
    [SerializeField] public GameObject triggerArea;

    [SerializeField] GameObject Bcollider;

    public static bool isMove = true;
    public static bool isDie;

    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMove == true)
        {
            if (!attackMode)
            {
                Move();
            }

            if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemeyAttack"))
            {
                SelectTarget();
            }

            if (inRange)
            {
                EnemyLogic();
            }
        }
        else if (!isMove)
        {
            Invoke("CanMove", 2f);
        }

        if (isDie)
        {
            anim.SetBool("isDieing", true);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(Bcollider.GetComponent<BoxCollider2D>());
            Invoke("Die", 1.2f);
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void CanMove()
    {
        isMove = true;
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            stopAttack();
        }
        if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            anim.SetBool("isAttacking", false);
        }
    }

    void Move()
    {
        anim.SetBool("isWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("isAttacking", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
