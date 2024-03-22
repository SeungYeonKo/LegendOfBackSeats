using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UI_OptionPopup : MonoBehaviour
{


    public void Open()
    {
        gameObject.SetActive(true);

    }

    public void Close()
    {
        gameObject.SetActive(false);
        Gamemanager.Instance.Continue();
    }


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnContinueButton()
    {
        Close();
    }


    public void OnResumeButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }


    public void OnTitlebutton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }


    public void OnExitButton()
    {
        Application.Quit();

#if UNITY_EDITOR
        // 유니티 에디터 에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }



}
