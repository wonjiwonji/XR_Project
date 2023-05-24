using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ScrollRect�� ��� �ޱ� ����
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
        parentScrollRect = GameObject.FindWithTag("NestedScrollManager").GetComponent<ScrollRect>();    // �θ��� ScrollRect
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�׸� �����ϴ� ���� �����̵��� ũ�� �θ� �巡�� ������ ��, ���� �̵��� ũ�� �ڽ��� �巡�� ������ ��
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
