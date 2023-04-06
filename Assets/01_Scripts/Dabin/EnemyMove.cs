using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("-------PlayLimit--------")]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;

    [Header("----------Move----------")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float StopTime;
    [SerializeField] private float checkPlayerTime;

    [Header("--------Collison---------")]
    [SerializeField] LayerMask raycastMask;
    [SerializeField] private float rayCastLength;
    [SerializeField] private float attackDistance;
    [SerializeField] private float coolTimer;
    [SerializeField] private GameObject Bcollider;
    [SerializeField] public GameObject hotZone;
    [SerializeField] public GameObject triggerArea;
    [HideInInspector] public bool inRange;
    [HideInInspector] public Transform target;

    [Header("--------Attack-------")]
    [SerializeField] private GameObject warningLine;

    [HideInInspector] public bool isMove = true;
    [HideInInspector] public bool follow = false;
    [HideInInspector] public bool isDie;
    [HideInInspector] public bool isShooting;
    
    private Animator anim;
    private float distance;
    public bool attackMode;
    private bool cooling;
    private bool checkPlayer;
    private float intTimer;

    private void Awake()
    {
        SelectTarget();
        intTimer = coolTimer;
        anim = GetComponentInChildren<Animator>();
        warningLine.SetActive(false);
        hotZone.SetActive(false);
        triggerArea.SetActive(true);
    }

    private void Update()
    {
        if (isMove && !isShooting)
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
        else if (!isMove && !checkPlayer)
        {
            Debug.Log("?");
            Invoke("CheckPlayer", checkPlayerTime);
        }
        else if(!isMove && checkPlayer)
        {
            Follow();
            isMove = true;
        }

        if (isDie)
        {
            anim.SetBool("isDieing", true);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(Bcollider.GetComponent<BoxCollider2D>());
            Invoke("Die", 1.2f);
        }
    }

    private void CheckPlayer()
    {
        checkPlayer = true;
        Debug.Log("CheckPlayer호출됨");
        Invoke("CanMove",checkPlayerTime + 0.5f);
    }

    private void Follow()
    {
        Debug.Log("!!");
        follow = true;
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

        if (distance > attackDistance && !isShooting)
        {
            StopAttack(); //총알 캐스팅 중에는 사거리는 무제한
        }
        if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            warningLine.SetActive(false);
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
        Debug.Log("attack 호출");
        warningLine.SetActive(true);
        coolTimer = intTimer;
        attackMode = true;

        isShooting = true;
        //anim.SetBool("isWalking", false);
        //anim.SetBool("isAttacking", true);
    }

    void CoolDown()
    {
        coolTimer -= Time.deltaTime;

        if (coolTimer <= 0 && cooling && attackMode)
        {
            cooling = false;
            coolTimer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false; // <- 이게 문제인데 훔...
        //isShooting = false;
        attackMode = false;
        anim.SetBool("isAttacking", false);
    }

    public void Cooling()
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
