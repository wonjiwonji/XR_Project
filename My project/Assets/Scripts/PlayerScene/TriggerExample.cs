using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 영역에 들어왔습니다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("플레이어가 영역을 떠났습니다.");
    }
}
