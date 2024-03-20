using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChestApperance : MonoBehaviour
{
    // public GameObject KeyChestPrefab;
    public GameObject AppearanceEffect;
    private GameObject _chest;
    public float AppearTime = 1.0f;
    public float CloseUpTime = 3.0f;
    public List<GameObject> TargetEnemies;
    private bool _hasTriggered = false;
    private CinemachineVirtualCamera _closeUpCamera;


    void Start()
    {
        _chest = transform.GetChild(0).gameObject;
        _chest.gameObject.SetActive(false);
        _closeUpCamera = transform.GetChild(1).gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // 모든 게임 오브젝트가 파괴되었는지 확인
        bool allDestroyed = true;
        foreach (GameObject monster in TargetEnemies)
        {
            if (monster != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed && !_hasTriggered)
        {
            StartCoroutine(ChestAppear_Coroutine());
            _hasTriggered = true;
        }
    }
    private IEnumerator ChestAppear_Coroutine()
    {
        _closeUpCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(AppearTime/2);
        //연기
        GameObject effect = Instantiate(AppearanceEffect);
        effect.transform.position = transform.position;
        yield return new WaitForSeconds(AppearTime/2);
        //상자 appear
        _chest.gameObject.SetActive(true);
        yield return new WaitForSeconds(CloseUpTime);
        _closeUpCamera.gameObject.SetActive(false);
    }
}
