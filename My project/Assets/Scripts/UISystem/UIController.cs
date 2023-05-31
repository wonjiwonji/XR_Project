using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    protected GameManager GameManager => GameManager.Instance;  // GameManager 싱글톤 인스턴스에 접근한다.

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += UpdateUI; // 상태 변화가 감지 되었을 때 해당 함수를 호출한다.
        
    }

    private void OnDisable()    // 지금 오브젝트가 OFF가 될 때
    {
        GameManager.OnGameStateChanged -= UpdateUI; // 선언한 이벤트를 삭제한다.
    }

    private void UpdateUI(GameManager.GameState state)
    {
        switch(state)
        {
            case GameManager.GameState.Start:
                Debug.Log("게임 시작 UI 업데이트");
                break;
            case GameManager.GameState.Playing:
                Debug.Log("게임 플레이 중 UI 업데이트");
                break;
            case GameManager.GameState.GameOver:
                Debug.Log("게임 오버 UI 업데이트");
                break;
        }
        
    }
}
