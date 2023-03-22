using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float rotSpeed = 0;

    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rotSpeed = 10000;
        }
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0); // 일정한 시간동안 일정 속도로 돌게해줌

        rotSpeed *= 0.99f; // 1퍼센트씩 매 턴 속도가 떨어짐

        
    }

}
