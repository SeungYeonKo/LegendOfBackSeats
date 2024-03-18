using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStartArea : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("트랩시작지점 밟음");
            // 씬 내의 모든 ArrowSpawner 인스턴스를 찾는 부분
            ArrowSpawner[] arrowSpawners = FindObjectsOfType<ArrowSpawner>();
            foreach (ArrowSpawner spawner in arrowSpawners)
            {
                spawner.StartSpawning(); // 각 ArrowSpawner에 대해 화살 발사를 시작
            }
           }
        }
    }