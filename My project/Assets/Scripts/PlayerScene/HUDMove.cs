using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI�� ����ϱ� ����

public class HUDMove : MonoBehaviour
{
    public Text textUI;

    private void Awake() // Start(){} ���� ���� ����Ǵ� �� // ���� ���� �� ������� �ۼ�
    {
        textUI = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f); // 5�� ������ ǥ�õǵ���
    }

    // Update is called once per frame
    void Update()
    {
        // UI�� ĵ���� ��¼�� �׷��� �����϶� local�̶�� ����������
        this.gameObject.transform.localPosition += new Vector3(0.0f, 0.2f, 0.0f);
    }
}
