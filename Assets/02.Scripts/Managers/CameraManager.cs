using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public List<CinemachineVirtualCamera> Cameras; // 전환할 카메라 리스트
    public CinemachineVirtualCamera DefaultCamera; // 기본 카메라
    public float TransitionDuration = 2f; // 카메라 전환 지속 시간

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SwitchToCamera(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= Cameras.Count)
        {
            return;
        }

        // 모든 카메라 비활성화
        foreach (var cam in Cameras)
        {
            cam.gameObject.SetActive(false);
        }

        // 선택한 카메라 활성화
        Cameras[cameraIndex].gameObject.SetActive(true);

        // 일정 시간 후 기본 카메라로 전환
        StartCoroutine(SwitchBackToDefaultCameraAfterDelay());
    }

    private IEnumerator SwitchBackToDefaultCameraAfterDelay()
    {
        yield return new WaitForSeconds(TransitionDuration);

        foreach (var cam in Cameras)
        {
            cam.gameObject.SetActive(false); // 모든 카메라 비활성화
        }

        DefaultCamera.gameObject.SetActive(true); // 기본 카메라 활성화
    }
}
