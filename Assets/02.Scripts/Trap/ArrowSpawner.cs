using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject TrapArrowPrefab;

    public float MinSpawnInterval = 6f;
    public float MaxSpawnInterval = 14f;
    public float SpawnTimer = 0f;
    public float SpawnInterval = 0f;

    public int PoolSize = 20;
    public List<TrapArrow> ArrowPool;



    private void Awake()
    {

        ArrowPool = new List<TrapArrow>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject TrapArrow = Instantiate(TrapArrowPrefab, this.transform);
            TrapArrow.SetActive(false);
            ArrowPool.Add(TrapArrow.GetComponent<TrapArrow>());
        }
    }

    private void Start()
    {
        SetRandomSpawnInterval();
        SpawnTimer = SpawnInterval;
    }

    private void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0f)
        {
            SpawnArrow();
            SetRandomSpawnInterval();
            SpawnTimer = SpawnInterval;
            Debug.Log("화살 랜덤 생성!");
        }
    }

    private void SetRandomSpawnInterval()
    {
        SpawnInterval = Random.Range(MinSpawnInterval, MaxSpawnInterval);
    }

    private void SpawnArrow()
    {
        foreach (TrapArrow arrow in ArrowPool)
        {
            if (!arrow.gameObject.activeInHierarchy)
            {
                arrow.gameObject.SetActive(true);
                arrow.gameObject.transform.position = this.transform.position;
                arrow.gameObject.transform.rotation = this.transform.rotation;
                return; // 활성화된 화살을 발사한 후 루프를 종료
            }
        }

        // 풀에 사용 가능한 화살이 없는 경우, 추가로 화살 생성 (선택적)
        GameObject newArrow = Instantiate(TrapArrowPrefab, this.transform.position, this.transform.rotation);
        ArrowPool.Add(newArrow.GetComponent<TrapArrow>());
        newArrow.SetActive(true);
    }
}
