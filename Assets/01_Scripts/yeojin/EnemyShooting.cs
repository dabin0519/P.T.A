using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField][Range(0f,10f)] private float drawRad;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = transform.Find("LineRenderer").GetComponentInChildren<LineRenderer>();

        _lineRenderer.positionCount = 2; // 라인 표현 점 개수
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Vector2 dir = (transform.position - _player.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, drawRad);

        if(hit)
        {
            Play(_player.position, hit.point);
        }
        else
        {
            Stop();
        }
    }

    public void Play(Vector3 from, Vector3 to) // 인덱스0: from, 인덱스1: to
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, from);
        _lineRenderer.SetPosition(1, to);
    }

    public void Stop()
    {
        _lineRenderer.enabled = false;
    }

    public IEnumerator WaitBeforeShoot(float time)
    {
        yield return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, drawRad);
        Gizmos.color = Color.green;
    }
#endif
}

