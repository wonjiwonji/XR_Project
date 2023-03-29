using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public bool move = false;
    public float blockMoveTime = 0.3f;
    public float blockMoveSpeed = 2.0f;

    private IEnumerator moveBlockTime(Vector3 dir)
    {
        move = true;

        float elapsedTime = 0.0f;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition + dir;

        while(elapsedTime < blockMoveTime)
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime / blockMoveTime);
            // 해당 시간 동안 어디까지 이동시켜줘 라는 함수

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = transform.position;
        move = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && move == false)
        {
            StartCoroutine(moveBlockTime(new Vector3(0.0f, 0.0f, 1.0f)));
        }
    }
}
