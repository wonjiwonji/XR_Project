using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI를 사용하기 위해

public class HUDMove : MonoBehaviour
{
    public Text textUI;

    private void Awake() // Start(){} 보다 빨리 시행되는 것 // 보통 세팅 할 내용들을 작성
    {
        textUI = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f); // 5초 정도만 표시되도록
    }

    // Update is called once per frame
    void Update()
    {
        // UI는 캔버스 어쩌고 그래서 움직일때 local이라고 명시해줘야함
        this.gameObject.transform.localPosition += new Vector3(0.0f, 0.2f, 0.0f);
    }
}
