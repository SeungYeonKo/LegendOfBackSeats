using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEndArea : MonoBehaviour
{

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("트랩끝남지점 밟음");
            ArrowSpawner[] arrowSpawners = FindObjectsOfType<ArrowSpawner>();
            foreach (ArrowSpawner spawner in arrowSpawners)
            {
                spawner.StopSpawning(); // 각 ArrowSpawner에 대해 화살 발사를 중지
            }
        }
    }
}