using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Array : MonoBehaviour // MonoBehaviour가 없으면 addComponent 불가!
                                        // 게임 오브젝트 생성을 위해 상속받은애임!
{

    public int[] Array = new int[5];


    // Start is called before the first frame update
    void Start()
    {
        Array[0] = 2;
        Array[1] = 10;
        Array[2] = 5;
        Array[3] = 15;
        Array[4] = 100;

    for (int i=0; i<Array.Length; i++)
    {
        Debug.Log(Array[i]);
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
