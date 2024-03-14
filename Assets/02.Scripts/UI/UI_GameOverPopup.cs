using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class UI_GameOverPopup : MonoBehaviour
{
    [Header ("게임오버 화면 페이드 인")]
    public CanvasGroup UIGameOverElement;
    private const float MinAlpha = 0f;  // 최소 알파
    private const float MaxAlpha = 1f;  // 최대 알파

    public float FadeInDuration = 2.0f;
    private float _fadeInProgress;

    private bool _isGameOverUIOpened;

    private void Start()
    {
        //UIGameOverElement.alpha = MinAlpha;
        _fadeInProgress = 0f;
        _isGameOverUIOpened = false;
    }

    private void Update()
    {
        /*
        if (_isGameOverUIOpened && _fadeInProgress < 1)
        {
            Debug.Log(_fadeInProgress);
            _fadeInProgress += Time.deltaTime / FadeInDuration;
            UIGameOverElement.alpha = Mathf.Lerp(MinAlpha, MaxAlpha, _fadeInProgress);
        }
        */
    }

    public void Open()
    {
        Debug.Log("게임 오버 오픈!");
        gameObject.SetActive(true);
        _isGameOverUIOpened = true;
        
    }


    public void Close()
    {
        _isGameOverUIOpened = false;
        gameObject.SetActive(false);
        //_fadeInProgress = 0f;
        //UIGameOverElement.alpha = MinAlpha;

    }


    private void Awake()
    {
        UIGameOverElement.alpha = MinAlpha;
        gameObject.SetActive(false);
        _isGameOverUIOpened= false;
    }



}
