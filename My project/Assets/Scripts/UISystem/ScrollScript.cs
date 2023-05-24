using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ScrollRect를 상속 받기 위해
using UnityEngine.EventSystems; // Scr

public class ScrollScript : ScrollRect
{
    bool forParent;
    NestScrollManager NM;
    ScrollRect parentScrollRect;

    // Start is called before the first frame update
    protected override void Start()
    {
        NM = GameObject.FindWithTag("NestedScrollManager").GetComponent<NestScrollManager>();
        parentScrollRect = GameObject.FindWithTag("NestedScrollManager").GetComponent<ScrollRect>();    // 부모의 ScrollRect
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그를 시작하는 순간 수평이동이 크면 부모가 드래그 시작한 것, 수직 이동이 크면 자식이 드래그 시작한 것
        forParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if(forParent)
        {
            NM.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        }
        else base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            NM.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        }
        else base.OnDrag(eventData);

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            NM.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        }
        else base.OnEndDrag(eventData);

    }
}
