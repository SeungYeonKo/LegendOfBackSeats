using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public GameObject Tutorial;

    private enum SceneNames
    {
        Lobby,
        Loading,
        Main
    }


    void Start()
    {
        Tutorial.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnGameStartButton()
    {
        SceneManager.LoadScene((int)SceneNames.Loading);
    }
    public void OnTutorialButton()
    {
        Tutorial.SetActive(true);
    }
    public void OnGameExitButton()
    {
        Debug.Log("게임 종료 버튼 클릭");
        Application.Quit();

#if UNITY_EDITOR
        // 유니티 에디터 에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

}
