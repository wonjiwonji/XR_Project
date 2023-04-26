using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    public enum PROJECTILETYPE      // enum으로 타입 선언
    {
        PLAYER,
        MONSTER
    }
    public Vector3 launchDirection; // 발사 방향
    public PROJECTILETYPE projectileType;

    private void FixedUpdate()
    {
        float moveAmount = 3 * Time.fixedDeltaTime; // 이동 속도 설정
        transform.Translate(launchDirection * moveAmount); // Translate 로 이동
    }

    // ctrl+k+o, ctrl+k+c // 충돌이 일어났을 경우..
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if (collision.gameObject.tag == "Object") // Tag 값이 오브젝트 인 경우
    //    {
    //        Destroy(this.gameObject); // 내 발사체가 없어진다.
    //    }
    //    if (collision.gameObject.tag == "Monster") // Tag 값이 Monster 인 경우
    //    {
    //        Destroy(this.gameObject); // 내 발사체가 없어진다.
    //        collision.gameObject.GetComponent<Monster>().Damaged(1);
    //    }
    //}

    private void OnTriggerEnter(Collider other)     // 트리거 예약 함수
    {
        if(other.CompareTag("Monster") && projectileType == PROJECTILETYPE.PLAYER)  // Tag를 검사한다.
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Monster>().Damaged(1);
            // 함수 호출을 위해 찾아야함 (연결 되어있지 않음) -> FindGameObjectWithTag ; 태그로 찾아서 연결
            GameObject Temp = GameObject.FindGameObjectWithTag("GameManager");
            Temp.GetComponent<HUDTextManager>().UpdateHUDTextSet("1", other.gameObject, new Vector3(0.0f, 10.0f, 0.0f));
        }

        if(other.CompareTag("Player") && projectileType == PROJECTILETYPE.MONSTER)  // Tag를 검사한다.
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<PlayerHp>().Damaged(1);
            GameObject Temp = GameObject.FindGameObjectWithTag("GameManager");
            Temp.GetComponent<HUDTextManager>().UpdateHUDTextSet("1", other.gameObject, new Vector3(0.0f, 10.0f, 0.0f));
        }
    }
}
