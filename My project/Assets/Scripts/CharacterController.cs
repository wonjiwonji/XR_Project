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
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0); // ������ �ð����� ���� �ӵ��� ��������

        rotSpeed *= 0.99f; // 1�ۼ�Ʈ�� �� �� �ӵ��� ������

        
    }

}
