using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMove : MonoBehaviour
{
    public float speed = 5.0f; // 오브젝트의 이동 속도
    public float distance = 10.0f; // 좌우로 움직일 최대 거리

    private Vector3 startPosition;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.Sin 함수를 이용하여 시간에 따라 -1과 1 사이의 값을 생성하고, 이를 거리에 곱해 위치를 계산
        float x = Mathf.Sin(Time.time * speed) * distance;

        // 계산된 x 위치를 기반으로 새로운 위치를 설정
        transform.position = startPosition + new Vector3(x, 0, 0);
    }
}
