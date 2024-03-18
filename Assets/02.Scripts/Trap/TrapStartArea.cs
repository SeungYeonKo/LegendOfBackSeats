using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStartArea : MonoBehaviour
{

    //private ArrowSpawner arrowSpawner;

    private void Start()
    {
        // ArrowSpawner 컴포넌트를 씬에서 찾기
        //arrowSpawner = FindObjectOfType<ArrowSpawner>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //ArrowSpawner arrowSpawner = GetComponent<ArrowSpawner>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("트랩시작지점 밟음");
           
        }
    }
}