using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // ���� ���� X
    // enemy�� �־���

    [SerializeField] private Transform _player;
    [SerializeField] private Color firstColor, secondColor;

    private LineRenderer _lineRenderer;
    private bool waitForShoot = false;

    private void Awake()
    {
        _lineRenderer = transform.Find("LineRenderer").GetComponentInChildren<LineRenderer>();

        _lineRenderer.positionCount = 2; // ���� ǥ�� �� ����
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (waitForShoot) return;
        FindPlayer();
    }

    private void FindPlayer()
    {
        Vector2 dir = (transform.position - _player.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        if (hit)
        {
            Play(transform.position, _player.position);
        }
    }

    public void Play(Vector3 from, Vector3 to) // �ε���0: from, �ε���1: to
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, from);
        _lineRenderer.SetPosition(1, to);

        StartCoroutine(WaitBeforeShoot(4f));
    }

    public void Stop()
    {
        _lineRenderer.enabled = false;
        StopAllCoroutines();
        waitForShoot = false;
    }

    public IEnumerator WaitBeforeShoot(float time)
    {
        _lineRenderer.startColor = firstColor;
        _lineRenderer.endColor = firstColor;
        yield return new WaitForSeconds(time);

        waitForShoot = true;

        _lineRenderer.startColor = secondColor;
        _lineRenderer.endColor = secondColor;
        yield return new WaitForSeconds(1f);
        Stop();
    }
}

