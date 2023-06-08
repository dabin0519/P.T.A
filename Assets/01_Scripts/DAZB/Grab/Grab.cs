using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    [SerializeField,Tooltip("그랩의 범위")] GameObject grabRange;
    [SerializeField, Tooltip("그랩의 사정거리")] GameObject grabRangeEndPos;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject playerObj;
    BoxCollider2D grabCollider;
    Player player;
    bool isGrab = false;
    bool isEnemy = false;
    Animator anim; // 플레이어 애니메이션

    private void Start() {
        anim = playerObj.GetComponent<Animator>();
        grabCollider = grabRange.GetComponent<BoxCollider2D>();
        player = GetComponentInParent<Player>();
        grabCollider.enabled = false;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && isGrab == false) {
            isGrab = true;
            StartCoroutine("ThrowGarb");
        }
    }

    private IEnumerator ThrowGarb() {
        grabCollider.enabled = true;
        player.SetState(PlayerState.Grab);
        StartCoroutine("GrabObjThrow");
        anim.SetTrigger("GrabThrow");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        grabCollider.enabled = false;
        anim.SetBool("IsGrab", false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 1.5f);
        player.SetState(PlayerState.Move);
        isGrab = false;
        isEnemy = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //anim.StopPlayback();
        StopCoroutine("GrabObjThrow");
        StartCoroutine("GetGrab", other);
        isEnemy = true;
        anim.SetBool("IsGrab", true);
        grabRange.transform.position = grabRangeEndPos.transform.position;
        //grabCollider.enabled = false;
    }

    private IEnumerator GrabObjThrow() {
        grabRange.transform.DOMove(grabRangeEndPos.transform.position, anim.GetCurrentAnimatorStateInfo(0).length);
        grabRange.transform.position = startPoint.transform.position;
        yield return null;
    }

    private IEnumerator GetGrab(Collider2D other) {
        grabRange.transform.DOMove(startPoint.transform.position, anim.GetCurrentAnimatorStateInfo(0).length);
        yield return null;
    }
}
