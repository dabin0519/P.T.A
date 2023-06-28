using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ThrowingWeaponManger : MonoBehaviour
{
    public GameObject weaponGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Throw(weaponGameObject, 5f);
    }

    public void Throw(GameObject weaponPrefab, float power = 5f, float destoryTime = 3f)
    {
        // ���� ���� ����
        if (GetScreenMouseIsUpOrDown() == 1)
        {
            Vector2 dir = GetMouseDirPosition();
            float time = 1.5f;
            //weaponGameObject.GetComponent<Rigidbody2D>().DOJump
            float geori = Vector3.Distance(transform.position, dir);
            dir.y = -0.12f;
            GameObject weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            //weapon.GetComponent<Rigidbody2D>().DOJump(dir, power / 2 + geori / 4, 1, time);
            weapon.GetComponent<Rigidbody2D>().AddForce(((new Vector3(dir.x, dir.y, 0) - transform.position).normalized + Vector3.up) * power * 40, ForceMode2D.Force);
            //weaponGameObject.transform.DOMoveY(dir.y * 5, time);
            Destroy(weapon, destoryTime);
        } else
        {
            Vector2 dir = GetScreenMouseIsLeftOrRight() == 1 ? Vector2.right : Vector2.left;
            GameObject weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            weapon.GetComponent<Rigidbody2D>().AddForce((dir * power * 60) + Vector2.up, ForceMode2D.Force);
            Destroy(weapon, destoryTime);
        }
    }


    // 1 = �� -1 = �Ʒ�
    private int GetScreenMouseIsUpOrDown()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        return mousePosition.y <= 0.5 ? -1 : 1;
    }

    // -1 = left 1 = right
    private int GetScreenMouseIsLeftOrRight()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        return mousePosition.x <= 0.5 ? -1 : 1;
    }


    private Vector2 GetMouseDirPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        //mousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        

        bool isLeft = false;
        bool isUp = false;
        // Left
        if (mousePosition.x <= 0.5) isLeft = true;
        if (mousePosition.y <= 0.5) isUp = true;

        //return new Vector2(isLeft ? -1 : 1, isUp ? 1 : -1);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //         Throw(weaponGameObject, 5f);
    // }
}
