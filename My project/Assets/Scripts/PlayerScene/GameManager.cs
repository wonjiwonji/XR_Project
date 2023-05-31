using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI �߰�
using System;   // System �߰�
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    protected SceneChanger SceneChanger => SceneChanger.Instance;   // �̱��� �ҷ�����

    public enum GameState   // ���� ���°� ����
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
            OnGameStateChanged?.Invoke(currentState);   // �̺�Ʈ�� null�� �ƴ� ��쿡�� �� �̺�Ʈ�� ȣ�� (���� ����)
        }
    }

    public void StartGame()
    {
        // ���� ���� ������ ���⿡ �ۼ�
        CurrentState = GameState.Playing;
    }

    public void GameOver()
    {
        // ���� ���� ������ ���⿡ �ۼ�
        CurrentState = GameState.GameOver;
        SceneChanger.LoadEndScene();
    }
    public GameManager() { } 
    public static GameManager Instance { get; private set; }    // �̱���ȭ
    public PlayerHp playerHp;   // �÷��̾��� Hp
    public Image playerHpUIImage;   // �÷��̾� Hp UI �̹���
    public Button BtnSample;    // UI ��ư ����

    /*private void Start()
    {
        this.BtnSample.onClick.AddListener(() =>    // Listenr�� ��ư ���
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

    private void OnDestroy()    // �� ������Ʈ�� �ı��� ���
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // �̺�Ʈ�� �����Ѵ�.
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
