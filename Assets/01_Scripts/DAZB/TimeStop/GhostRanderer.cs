using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRanderer : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    PlayerMove _player;

    private void Awake() {
        _player = GameObject.Find("Player").GetComponentInChildren<PlayerMove>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        transform.position = _player.transform.position;
        transform.localScale = _player.transform.localScale;
        _spriteRenderer.sprite = _player.SpriteRend.sprite;
        _spriteRenderer.color = color;
    }

    private void Update() {
        _destroyTime -= Time.deltaTime;
        if (_destroyTime <= 0) {
            Destroy(gameObject);
        }
    }
}
