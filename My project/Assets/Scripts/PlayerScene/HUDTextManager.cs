using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 사용

public class HUDTextManager : MonoBehaviour
{

    public Text hudText;
    public GameObject character;    // 캐릭터 게임 오브젝트를 참조
    public Vector3 offset;  // 캐릭터 머리 위에 텍스트를 표시할 오프셋

    public GameObject HudTextUp;    // 올라가는 텍스트 프리팹
    public GameObject canvasObject; // Canvas Tranform 좌표
    

    // Update is called once per frame
    void Update()
    {
        if(character != null)
        {
        // 캐릭터 머리 위에 텍스트 표시
        Vector3 characterHeadPosition = character.transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(characterHeadPosition); // 3D Position -> 2D
        hudText.transform.position = screenPosition + offset;
        }
    }

    public void UpdateHUDTextSet(string newText, GameObject target, Vector3 TargetOffset)
    {
        Vector3 TargetPosition = target.transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(TargetPosition);    // 3D Position -> 2D
        GameObject temp = (GameObject)Instantiate(HudTextUp);
        temp.transform.SetParent(canvasObject.transform, false);
        temp.transform.position = screenPosition + TargetOffset;

        // HUDMove Class
        temp.GetComponent<HUDMove>().textUI.text = newText;
    }

}
