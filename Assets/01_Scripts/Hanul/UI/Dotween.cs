using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dotween : MonoBehaviour
{
    bool isDown;
    void Start()
    {
        gameObject.SetActive(false);
        isDown = false;
        //Tween mytween = transform.DORotate(new Vector3(0, 0, 720), 10f, RotateMode.FastBeyond360);
        //mytween.OnComplete(() =>
        //{
        //    gameObject.SetActive(false);
        //});
        //transform.DOMove(new Vector2(2, 4), duration: 3f);
        //transform.DOMoveY(-4.5f, 6);

        //GetComponent<SpriteRenderer>().DOColor(Color.red, 3f);
        //GetComponent<SpriteRenderer>().DOFade(0.5f, 3f);

    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    transform.DOScale(3, 0.5f);
        //}

        //else if (Input.GetKeyDown(KeyCode.I))
        //{
        //    transform.DOScale(1, 0.5f);
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    transform.DOShakePosition(0.5f);
        //}
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isDown)
            {
                gameObject.SetActive(true);
                transform.DOScale(new Vector3(1, 1, 1), 0.5f);
                isDown = true;
            }
            else if (isDown)
            {
                transform.DOScale(new Vector3(0, 0, 0), 0.5f);
                isDown = false;
            }
        }

    }

}
