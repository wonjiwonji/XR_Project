using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 추가
using System;   // System 추가
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected SceneChanger SceneChanger => SceneChanger.Instance;   // 싱글톤 불러오기

    public enum GameState   // 게임 상태값 설정
    {
        Start,
        Playing,
        GameOver
    }

    public event Action<GameState> OnGameStateChanged;

    public GameState currentState = GameState.Start;

    public GameState CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;
            OnGameStateChanged?.Invoke(currentState);   // 이벤트가 null이 아닌 경우에만 이 이벤트를 호출 (변경 감지)
        }
    }

    public void StartGame()
    {
        // 게임 시작 로직을 여기에 작성
        CurrentState = GameState.Playing;
    }

    public void GameOver()
    {
        // 게임 오버 로직을 여기에 작성
        CurrentState = GameState.GameOver;
        SceneChanger.LoadEndScene();
    }
    public GameManager() { } 
    public static GameManager Instance { get; private set; }    // 싱글톤화
    public PlayerHp playerHp;   // 플레이어의 Hp
    public Image playerHpUIImage;   // 플레이어 Hp UI 이미지
    public Button BtnSample;    // UI 버튼 선언

    /*private void Start()
    {
        this.BtnSample.onClick.AddListener(() =>    // Listenr로 버튼 등록
        {
            Debug.Log("Button Check");
        });
    }*/

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        init();
        
    }

    private void OnDestroy()    // 이 오브젝트가 파괴될 경우
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // 이벤트를 삭제한다.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            init();
        }
    }
    private void init()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        playerHpUIImage = GameObject.FindGameObjectWithTag("UIHealthBar").GetComponent<Image>();
        playerHp.Hp = 100;
        CurrentState = GameState.Start;
    }
    private void Update()
    {
        playerHpUIImage.fillAmount = (float)playerHp.Hp / 100.0f;
    }
}
