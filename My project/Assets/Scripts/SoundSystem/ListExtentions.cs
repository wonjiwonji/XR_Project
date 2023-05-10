using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;  // 유니티 엔진에서 제공해주는 랜덤

public static class ListExtentions
{
    public static T RandomItem<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("List is Empty");

        var randomIndex = Random.Range(0, list.Count);  // 유니티 제공해주는 랜덤에서 List 요소 하나를 뽑아서 전달
        return list[randomIndex];   // 리스트 랜덤 요소 반환
    }
}
