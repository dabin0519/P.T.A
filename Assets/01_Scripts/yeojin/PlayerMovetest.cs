using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovetest : MonoBehaviour
{
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 newInput = new Vector2(h, v);

        rigid.velocity = newInput.normalized * 5f;
    }
}
