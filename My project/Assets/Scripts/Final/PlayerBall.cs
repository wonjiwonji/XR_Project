using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float JumpPower;
    public int itemCount;   // 점수
    public GameManagerLogic manager;

    bool isJump;    // 무한 점프 방지
    Rigidbody rigid;
    AudioSource audio01;  // 효과음


    void Awake()
    {
        isJump = false; // 처음은 점프가 안된 상태 (초기화)
        rigid = GetComponent<Rigidbody>();
        audio01 = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if(Input.GetButtonDown("Jump") && !isJump) // 점프 하고 && 점프 중이 아니라면
        {
            isJump = true; // 상태 변경
            rigid.AddForce(new Vector3(0,JumpPower,0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h,0,v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")    // Player가 바닥에 닿으면(점프 끝)
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")  // 플레이어가 아이템을 먹으면
        {
            itemCount++;
            audio01.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish")
        {
            if(itemCount == manager.totalItemCount) // 아이템 다 먹으면
            { // Game Clear && Next Stage
                if(manager.stage == 2)
                    SceneManager.LoadScene(0); 
                else
                    SceneManager.LoadScene(manager.stage +1);
            }
            else    // 아이템 다 못먹으면
            { // SceneManager.LoadScene(0);
                SceneManager.LoadScene(manager.stage);
            }
        }
    }
}
