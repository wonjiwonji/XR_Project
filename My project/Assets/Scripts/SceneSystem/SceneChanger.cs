using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  // 유니티 Scene 이동을 위해서 가져옴
using UnityEngine;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public float transitionDuration = 1f;

    public CanvasGroup canvasGroup;

    public SceneChanger() { }

    public static SceneChanger Instance { get; private set; }    // 싱글톤화

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
    }

    private void OnDestroy()    // 이 오브젝트가 파괴될 경우
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // 이벤트를 삭제한다.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvasGroup = FindObjectOfType<CanvasGroup>();
        if(canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
   
    private void PlayTransitionAnimation(System.Action callback)
    {
        if(canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0f, transitionDuration).OnComplete(() =>
            {
                // 알파 값을 1 -> 0으로 1초 동안 이동하고 완료되면 등록된 Callback으로 이동
                callback.Invoke();
            });
        }
        else
        {
            callback.Invoke();
        }
    }

    public void LoadLobbyScene()
    {
        PlayTransitionAnimation(() =>
        {
            SceneManager.LoadScene("LobbyScene");
        });
    }

    public void LoadGameScene()
    {
        PlayTransitionAnimation(() =>
        {
            SceneManager.LoadScene("Scene_0510");
        });
    }

    public void LoadEndScene()
    {
        PlayTransitionAnimation(() =>
        {
            SceneManager.LoadScene("EndScene");
        });
    }
}
