using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    public Transform player;
    public Transform lookRot;

    [Range(0f, 20f)]
    public float viewRadius = 3f;
    [Range(0f, 360f)] 
    [SerializeField] private float viewAngle;

    private float angle = 1f;
    private bool isPlayer = false;
    public bool isRot = true;

    private void Awake()
    {
        angle = transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        CCTVLook();
    }

    /*
    
    private void CCTVLookatPlayer()
    {
        Vector3 dir = lookRot.position - player.position;
        float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(new Vector3(0, 0, degree));
        transform.rotation = Quaternion.Lerp(transform.rotation, target, speed * Time.deltaTime);

        angle = transform.rotation.eulerAngles.z;
    }
    */

    private void CCTVLook()
    {
        Quaternion target = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, target, speed * Time.deltaTime);
        if(isRot) angle += Time.deltaTime * 15f;
        else angle -= Time.deltaTime * 15f;

        isRot = (angle < 0 || angle > 80) ? !isRot : isRot;
    }
}

