using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPingPong : MonoBehaviour
{
    public RectTransform StartRectTransform; // 왕복 운동의 시작 위치
    public RectTransform EndRectTransform;   // 왕복 운동의 끝 위치
    public float speed = 1.0f;    // 왕복 운동의 속도 조절 변수

    private RectTransform rectTransform; // 현재 오브젝트의 RectTransform 컴포넌트
    private float lerpTime;              // Lerp 함수에 사용될 시간 변수

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // 시작 시, 현재 오브젝트의 RectTransform 컴포넌트를 가져온다.
    }

    void Update()
    {
        // Time.time에 speed를 곱한 값을 1로 나눈 나머지를 lerpTime에 저장하여 시간에 따라 0에서 1 사이를 왕복하도록 한다.
        lerpTime = Mathf.PingPong(Time.time * speed, 1);

        // Vector2.Lerp를 사용하여 startPosition과 endPosition 사이를 lerpTime에 따라 부드럽게 왕복 운동한다.
        rectTransform.anchoredPosition = Vector2.Lerp(StartRectTransform.anchoredPosition, EndRectTransform.anchoredPosition, lerpTime);
    }
}
