using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Test2 : MonoBehaviour
{
    public int hp = 180;
    public Text textHpUI; // Hp UI 표시 선언
    public Text textStateUI; // State 표시 선언

    // Start is called before the first frame update
    void Start() // 시작할 때 한번만 실행
    {
       
        
    }

    // Update is called once per frame
    void Update() // 무한 루프
    {
        //======================================================
        // UI 내용
        //======================================================
        textHpUI.text = hp.ToString(); // hp 문자열로 변경

        // 클릭이벤트 쌉~가능 // 외부에서 값 조정
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼
        {
            hp += 10;
        }

        //======================================================
        // Input 내용
        //======================================================
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼
        {
            hp -= 10;
        }

        if (hp <= 50)
        {
            textStateUI.text = "Run!!";
            /*Debug.Log("도망!");*/
        }
        else if (hp >= 200)
        {
            textStateUI.text = "Attack!!";
            /*Debug.Log("공격!");*/
        }
        else
        {
            textStateUI.text = "Defance!!";
            /*Debug.Log("방어!");*/
        }
    }
}
