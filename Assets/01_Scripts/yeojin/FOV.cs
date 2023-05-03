using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [Range(0f, 360f)]
    [SerializeField] private float horizonViewAngle = 0f;
    [SerializeField] private int viewRadius = 1;

    [Range(-180f, 180f)]
    [SerializeField] private float viewRotate = 0f;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    private float horizontalViewHalfAngle = 0f;

    [SerializeField] private Transform player;
    [SerializeField] private bool isPlayer;

    private void Awake()
    {
        horizontalViewHalfAngle = horizonViewAngle * 0.5f;
    }

    private void Update()
    {
        FindTarget();
    }

    private Vector3 AngleToDir(float angle)
    {
        float r = (angle - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(r), Mathf.Cos(r), 0f);
    }

    private void FindTarget()
    {
        Vector2 lookDir = AngleToDir(viewRotate);
        Vector2 dir = (player.position - transform.position).normalized;
        
        float dot = Vector2.Dot(lookDir, dir);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        if (angle <= horizontalViewHalfAngle) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius);
            if (hit)
            {
                Debug.DrawLine(transform.position, player.position, Color.yellow);
                isPlayer = true;
                return;
            }
        }
        isPlayer = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        horizontalViewHalfAngle = horizonViewAngle * 0.5f;

        Vector3 rightBoundary = AngleToDir(-horizontalViewHalfAngle + viewRotate);
        Vector3 leftBoundary = AngleToDir(horizontalViewHalfAngle + viewRotate);
        Vector3 lookDir = AngleToDir(viewRotate);

        Debug.DrawRay(transform.position, leftBoundary * viewRadius, Color.red);
        Debug.DrawRay(transform.position, lookDir * viewRadius, Color.blue);
        Debug.DrawRay(transform.position, rightBoundary * viewRadius, Color.red);
    }
#endif
}
