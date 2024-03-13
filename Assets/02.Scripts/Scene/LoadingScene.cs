using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LobbyScene;
using UnityEngine.SceneManagement;



public class LoadingScene : MonoBehaviour
{
    

    void Start()
    {
        StartCoroutine(WaitAndLoadNextScene(3)); // 5초 대기 후 다음 씬 로드
    }

    IEnumerator WaitAndLoadNextScene(float waitTime)
    {

        yield return new WaitForSecondsRealtime(waitTime); // 파라미터로 받은 시간(초)만큼 대기

        // 다음 씬 로드
        // 'NextScene'을 로드하고자 하는 씬의 이름 또는 인덱스로 교체하세요.
        SceneManager.LoadScene("SuBin Academy");
    }

}
