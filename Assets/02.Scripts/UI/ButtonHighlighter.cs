using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI StartGameText; // 버튼의 텍스트 컴포넌트
    public Image PinImage; // 보여질 이미지 컴포넌트

    public Color highlightColor; // 강조될 때의 텍스트 색상
    private Color originalColor; // 원래의 텍스트 색상

    void Start()
    {
        if (StartGameText != null)
        {
            // 초기 텍스트 색상 저장
            originalColor = StartGameText.color;
        }
        if (PinImage != null)
        {
            // 이미지를 숨긴다.
            PinImage.enabled = false;
        }
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 커서가 버튼 영역 안으로 들어왔을 때
        if (StartGameText != null)
        {
            StartGameText.color = highlightColor; // 텍스트 색상 변경
        }
        if (PinImage != null)
        {
            // 이미지를 보여준다.
            PinImage.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 커서가 버튼 영역 밖으로 나갔을 때
        if (StartGameText != null)
        {
            StartGameText.color = originalColor; // 원래의 텍스트 색상으로 변경
        }
        if (PinImage != null)
        {
            // 이미지를 다시 숨긴다.
            PinImage.enabled = false;
        }
    }
}
