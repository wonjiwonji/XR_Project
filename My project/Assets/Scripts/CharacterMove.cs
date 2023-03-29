using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float characterSpeed = 0;
    public GameObject characerRender;

    void Start()
    {
        StartCoroutine(Fade());
        
    }
    IEnumerator Fade()
    {
        Color c = characerRender.GetComponent<Renderer>().material.color; // Material ����

        for (float fadeoffset = 1.0f; fadeoffset >= 0; fadeoffset -=0.1f)
        {
            c.b = fadeoffset; // Blue �� ����
            c.g = fadeoffset; // Green �� ����

            characerRender.GetComponent<Renderer>().material.color = c;

            yield return new WaitForSeconds(1.0f); // For ������ 1�ʸ��� ���ư��� ��
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            characterSpeed = 5.0f;
        }
        transform.Translate(0, 0, characterSpeed * Time.deltaTime);

        characterSpeed *= 0.99f;
    }

}
