using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dsdsdsds : MonoBehaviour
{

    [SerializeField] GameObject grabRange;
    [SerializeField] GameObject grabDistance;
    //[SerializeField] GameObject startPoint;
    Player playerStat;
    BoxCollider2D grabCollider;
    Animator grabAnim;
    bool isEnemy =false;

    private void Start() {
        grabAnim = GetComponent<Animator>();
        grabCollider = grabRange.GetComponent<BoxCollider2D>();
        playerStat = GetComponentInParent<Player>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && playerStat.GetState() != PlayerState.Grab) {
            StartCoroutine("ThrowGrab");
        }
    }

    private IEnumerator ThrowGrab() {
        playerStat.SetState(PlayerState.Grab);
        //grabCollider.enabled = true;
        grabRange.SetActive(true);
        grabAnim.SetTrigger("GrabThrow");
        float duration = 0.5f;
        Vector3 startPos = grabRange.transform.position;
        Vector3 endPos = grabDistance.transform.position;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            grabRange.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            if (isEnemy) yield break;
            yield return null;
        }
        //yield return new WaitForSeconds(grabAnim.GetCurrentAnimatorStateInfo(0).length);
        playerStat.SetState(PlayerState.Move);
        grabRange.transform.position = new Vector3(transform.position.x, transform.position.y - 0.6f);
       // grabCollider.enabled = false
       grabRange.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.transform.SetParent(grabRange.transform);
        StartCoroutine("GetGrab", other);
    }

    private IEnumerator GetGrab(Collider2D other) {
        grabAnim.SetBool("IsGrab", true);
        float duration = grabAnim.GetCurrentAnimatorStateInfo(0).length;
        Vector3 startPos = grabRange.transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y - 0.6f);
        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            grabRange.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            //other.transform.position = grabRange.transform.position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isEnemy = false;
        other.transform.SetParent(null);
        grabAnim.SetBool("IsGrab", false);
        //grabCollider.enabled = false;
        grabRange.SetActive(false);
        yield return null;
    }
}
