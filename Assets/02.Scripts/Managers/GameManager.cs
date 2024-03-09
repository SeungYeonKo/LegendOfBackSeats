using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GameState
{
    Ready, // 대기
    Go, // 시작
    Pause,
    Over,  // 게임오버
}
public class Gamemanager : MonoBehaviour
{
    // 게임의 상태는 처음에 "준비"상태
    public static Gamemanager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Ready;


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

    public void GameOver()
    {
        Time.timeScale = 0f;
        State = GameState.Over;

    }


    public void Pause()
    {
        State = GameState.Pause;
        Time.timeScale = 0f;

    }
    public void Continue()
    {
        State = GameState.Go;
        Time.timeScale = 1f;

    }


}
