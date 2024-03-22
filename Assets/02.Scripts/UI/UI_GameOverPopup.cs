
using UnityEngine;


public class UI_GameOverPopup : MonoBehaviour
{
    //[Header ("게임오버 화면 페이드 인")]
    public CanvasGroup UIGameOverElement;
    private const float MinAlpha = 0f;  // 최소 알파
    private const float MaxAlpha = 1f;  // 최대 알파

    public float ActiveDuration = 4f;
    private float _fadeInProgress;

    private bool _isGameOverUIOpened;

    private void Start()
    {
        UIGameOverElement.alpha = MinAlpha;
        //_fadeInProgress = 0f;
    }

    public void Open()
    {   
        //gameObject.SetActive(true);
        UIGameOverElement.alpha = MinAlpha;

     //   StartCoroutine(GameOverUIOpen_Coroutine());
    }


    public void Close()
    {
        gameObject.SetActive(false);
        //_fadeInProgress = 0f;
        UIGameOverElement.alpha = MinAlpha;
    }


    private void Awake()
    {
        //UIGameOverElement.alpha = MinAlpha;
    }



}
