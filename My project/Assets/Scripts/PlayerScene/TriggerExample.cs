using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ������ ���Խ��ϴ�.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�÷��̾ ������ �������ϴ�.");
    }
}
