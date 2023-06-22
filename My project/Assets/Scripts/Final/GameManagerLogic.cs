using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // UI 쪽 정보 가져옴 (먹은 아이템 표시)

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text playerCountText;

    void Awake()
    {
        stageCountText.text = "/ "+totalItemCount;
    }

    public void GetItem(int count)   // 플레이어가 아이템 먹으면
    {
        playerCountText.text = count.ToString();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
