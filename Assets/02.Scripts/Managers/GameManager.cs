using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Timeline;


public enum GameState
{
    CutScene, 
    Go, // 시작
    Pause,
    Over,
    Ending,
}


public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance { get; private set; }
    public GameState State = GameState.Go;

    public UI_OptionPopup OptionUI;
    public GameObject GameOverUI;
    [HideInInspector]
    public PlayableDirector PlayableDirector;
    public List<TimelineAsset> TimeLines;


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
       PlayableDirector = GetComponent<PlayableDirector>();
}
    private void Start()
    {
        PlayableDirector.Play(TimeLines[0]);
        OnCutScene();
    }

    private void Update()
    {
        if (State == GameState.Pause)
        {
            Pause();

        }
        else if (State == GameState.Go)
        {
            Continue();
        }

        else if (State == GameState.Over)
        {
            GameOver();
        }
        else if (State == GameState.CutScene)
        {
            OnCutScene();
        }
        else if (State == GameState.Ending)
        {
            OnEnding();
        }
    }

    public void GameOver()
    {
        //Debug.Log("게임 오버");
        Time.timeScale = 0f;
        State = GameState.Over;
        StartCoroutine(GameOverUIPopup_Coroutine());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private IEnumerator GameOverUIPopup_Coroutine()
    {
        yield return new WaitForSecondsRealtime(3f);

        GameOverUI.SetActive(true);
    }
    public void OnCutScene()
    {
        Time.timeScale = 1f;
        State = GameState.CutScene;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (PlayableDirector.state != PlayState.Playing)
        {
            Continue();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayableDirector.time = 15.25f;
        }
    }
    public void OnEnding()
    {
        Time.timeScale = 0.5f;
        State = GameState.Ending;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
/*        if (PlayableDirector.state == PlayState.Playing)
        {
            OnCutScene();
        }*/
    }

/*    public void OnOptionEscKeyClickeed()
    {
        //Debug.Log("옵션창 나타내기");
        Pause();
        OptionUI.Open();
    }*/



}

