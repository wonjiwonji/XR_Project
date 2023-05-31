using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI를 사용하기 위한 선언

public class UIEndScene : MonoBehaviour
{
    protected SceneChanger SceneChanger => SceneChanger.Instance;   // 싱글톤 불러오기
    public Button gameButton;   // 선언한 버튼 설정

    // Start is called before the first frame update
    void Start()
    {
        gameButton.onClick.AddListener(OnGameButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGameButtonClick()
    {
        SceneChanger.LoadLobbyScene();
    }
}

