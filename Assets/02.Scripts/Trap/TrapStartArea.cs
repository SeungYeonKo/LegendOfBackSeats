using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapStartArea : MonoBehaviour
{
    public TextMeshProUGUI AvoidArrowTextUI;
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        AvoidArrowTextUI.text = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(ShowAvoidArrowMessage());
                Debug.Log("트랩시작지점 밟음");
                // 씬 내의 모든 ArrowSpawner 인스턴스를 찾는 부분
                ArrowSpawner[] arrowSpawners = FindObjectsOfType<ArrowSpawner>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            foreach (ArrowSpawner spawner in arrowSpawners)
                {
                    spawner.StartSpawning(); // 각 ArrowSpawner에 대해 화살 발사를 시작
                }
            _impulseSource.GenerateImpulse(1.5f);
        }
        }

    IEnumerator ShowAvoidArrowMessage()
    {
        AvoidArrowTextUI.gameObject.SetActive(true);
        AvoidArrowTextUI.text = "날아오는 마법 화살을 피하세요 !";
        yield return new WaitForSeconds(2f);
        AvoidArrowTextUI.gameObject.SetActive(false);
         }
    }