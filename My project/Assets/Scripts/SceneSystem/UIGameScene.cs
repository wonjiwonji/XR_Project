using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    protected GameManager GameManager => GameManager.Instance;   // �̱��� �ҷ�����
    public Button gameStartButton;   // ������ ��ư ����
    public Button gameOverButton;   // ������ ��ư ����

    // Start is called before the first frame update
    void Start()
    {
        gameStartButton.onClick.AddListener(gameStartButtonClick);
        gameOverButton.onClick.AddListener(gameOverButtonClick);
    }

    private void gameStartButtonClick()
    {
        GameManager.StartGame();
    }

    private void gameOverButtonClick()
    {
        GameManager.GameOver();
    }
}