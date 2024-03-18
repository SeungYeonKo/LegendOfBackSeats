using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public enum GameState
{
    CutScene, // 대기
    Go, // 시작
    Pause,
    Over,  // 게임오버
}


public class Gamemanager : MonoBehaviour
{
    // 게임의 상태는 처음에 "준비"상태
    public static Gamemanager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Go;

    public UI_OptionPopup OptionUI;
    public GameObject GameOverUI;

    private bool _isOptionOpened;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (State == GameState.Pause)
        {
            Pause();

        }
        if (State == GameState.Go)
        {
            Continue();
        }

        if (State == GameState.Over)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        //Debug.Log("게임 오버");
        Time.timeScale = 0f;
        State = GameState.Over;
        Debug.Log("Gameover Coroutine");
        StartCoroutine(GameOverUIPopup_Coroutine());
    }
    private IEnumerator GameOverUIPopup_Coroutine()
    {
        yield return new WaitForSecondsRealtime(3f);

        GameOverUI.SetActive(true);
    }
    public void OnCutScene()
    {
        State = GameState.CutScene;
    }
    public void Pause()
    {
 
        State = GameState.Pause;
        Time.timeScale = 0f;
        OptionUI.Open();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Continue()
    {
        State = GameState.Go;
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            OptionUI.Open();
            Debug.Log("Pause Menu");
        }
    }

/*    public void OnOptionEscKeyClickeed()
    {
        //Debug.Log("옵션창 나타내기");
        Pause();
        OptionUI.Open();
    }*/



}

