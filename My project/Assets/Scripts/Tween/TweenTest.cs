using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // Tween 쓸거임

public class TweenTest : MonoBehaviour
{

    public bool isPunch = false;
    Sequence sequence;
    private Renderer renderer;  // 색을 가져오기 위해서

    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMoveX(5, 2);    // X 축으로 5만큼 2초동안 이동
        //transform.DORotate(new Vector3(0, 0, 180), 2);  // Z축 중심으로 180도 회전 2초동안
        //transform.DOScale(new Vector3(2, 2, 2), 2); // X 2배 확대 2초동안

        // Sequence 에 넣으면 아래 추가한 순서대로 동작함.
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveX(5, 2));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 180), 2));
        sequence.Append(transform.DOScale(new Vector3(2, 2, 2), 2));
        sequence.SetLoops(-1, LoopType.Yoyo);


        //transform.DOMoveX(5, 2).SetEase(Ease.OutBounce).OnComplete(DeactivateObject);
        //transform.DOShakeRotation(2, new Vector3(0, 0, 180), 10, 90); // 강도, 랜덤성, FadeOut 정도 등을 넣을 수 있음


        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            sequence.Kill();    // 멈추도록
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isPunch)
            {
                isPunch = true;
                transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f, 10, 1).OnComplete(EndPunch);

                Color color = new Color(Random.value, Random.value, Random.value);  // rgb값을 랜덤으로 (0~10?)

                renderer.material.DOColor(color, 0.1f)
                    .SetEase(Ease.InOutQuad)
                    .SetAutoKill(false);

                renderer.material.DOPlay();
            }
        }
    }

    void EndPunch()
    {
        isPunch = false;
    }

    void DeactivateObject() // 종료 되면
    {
        gameObject.SetActive(false); // object 꺼지게
    }

}
