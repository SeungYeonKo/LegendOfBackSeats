using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLoadingUI : MonoBehaviour
{
    public float speed = 200f; // 회전 속도 조절

    void Update()
    {
        // Time.deltaTime을 곱해 프레임률에 관계없이 일정한 속도로 회전
        transform.Rotate(0, 0, -speed * Time.unscaledDeltaTime);
    }
}
