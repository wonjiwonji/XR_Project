using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Scrollbar scrollbar;
    public Transform contentTr;

    public RectTransform[] BtnRect, BtnImageRect;

    const int SIZE = 4;
    float[] pos = new float[SIZE];
    float distance, curPos, targetPos;
    bool isDrag;
    int targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        // 거리에 따라서 0~1인 POS 대입
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++)
        {
            pos[i] = distance * i;
        }
    }

    float SetPos()
    {
        // 절반 거리를 기준으로 가까운 위치를 반환
        for(int i=0; i<SIZE; i++)
        {
            if(scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        }
        return 0;
    }

    public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();
    public void OnDrag(PointerEventData eventData) => isDrag = true;
   
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();

        // 절반 거리를 넘지 않아도 마우스를 빠르게 이동하면
        if (curPos == targetPos)
        {
            if (eventData.delta.x > 20 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            // -> 으로 가려면 목표가 하나 증가
            if (eventData.delta.x < -20 && curPos - distance <= 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
        VerticalScrollUp();
    }

    void VerticalScrollUp()
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (contentTr.GetChild(i).GetComponent<ScrollScript>() && curPos != pos[i] && targetPos == pos[i])
                contentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
        }
    }

    public void TabClick(int n)
    {
        curPos = SetPos();
        targetIndex = n;
        targetPos = pos[n];
        VerticalScrollUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

            for (int i = 0; i<SIZE; i++)
            {
                if(i==targetIndex)
                {
                    BtnRect[i].sizeDelta = new Vector2(700, BtnRect[i].sizeDelta.y);
                }
                else
                {
                    BtnRect[i].sizeDelta = new Vector2(400, BtnRect[i].sizeDelta.y);
                }
            }
        }
    }
}
