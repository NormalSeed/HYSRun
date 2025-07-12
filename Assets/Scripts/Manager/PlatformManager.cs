using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // 일정 주기로 플랫폼을 생성하는 매니저
    // 생성 타이머와 스테이지 종료 타이머를 가짐
    // 월드 포지션 기준 x = 20, y = -5 ~ 0 사이의 위치에 생성
    // 생성 시 이전 생성된 플랫폼보다 position.y의 값이 2보다 크면 안됨
    // 생성 시 이전 생성된 플랫폼에 몬스터가 있었다면 몬스터가 플랫폼 위에 있으면 안됨
    // 플랫폼 이동 속도는 시간에 따라 점점 빨라짐
    [Header("자동생성 플랫폼")]
    [SerializeField] List<PooledObject> platforms = new List<PooledObject>();
    List<ObjectPool> platformPools = new List<ObjectPool>();
    private ObjectPool platform1Pool;
    private ObjectPool platform2Pool;
    private ObjectPool platform3Pool;
    private ObjectPool platform4Pool;
    private ObjectPool platform5Pool;
    private ObjectPool platform6Pool;

    [SerializeField] private float createTimer;
    [SerializeField] private float stageEndTimer;
    [SerializeField] private float platformSpd;

    private bool isSpawned;
    private float previousY = 0;

    private void Awake()
    {
        platform1Pool = new ObjectPool(transform, platforms[0], 5);
        platform2Pool = new ObjectPool(transform, platforms[1], 5);
        platform3Pool = new ObjectPool(transform, platforms[2], 5);
        platform4Pool = new ObjectPool(transform, platforms[3], 5);
        platform5Pool = new ObjectPool(transform, platforms[4], 5);
        platform6Pool = new ObjectPool(transform, platforms[5], 5);

        platformPools.Add(platform1Pool);
        platformPools.Add(platform2Pool);
        platformPools.Add(platform3Pool);
        platformPools.Add(platform4Pool);
        platformPools.Add(platform5Pool);
        platformPools.Add(platform6Pool);
    }

    private void Start()
    {
        createTimer = 3f;
        stageEndTimer = 300f;
        platformSpd = 3f;
        isSpawned = false;
    }

    private void Update()
    {
        if (createTimer > 0)
            createTimer -= Time.deltaTime;

        if (createTimer <=0)
        {
            CreatePlatform();
            createTimer = 3.5f;
        }
    }

    private void CreatePlatform()
    {
        int index = Random.Range(0, platforms.Count);
        Debug.Log($"생성 플랫폼 번호 : {index + 1}");
        Platform platform = GetPlatform(index);
        float y = Random.Range(-4f, 1.5f);
        while (y > previousY + 2)
        {
            y = Random.Range(-4f, 1.5f);
        }

        platform.Launch(y, platformSpd);

        MonsterSpawner spawner = platform.GetComponent<MonsterSpawner>();

        if (spawner != null && !isSpawned)
        {
            spawner.SpawnEnemy();
            isSpawned = true;
        }
        else if (spawner != null && isSpawned)
        {
            isSpawned = false;
        }
    }

    public Platform GetPlatform(int index)
    {
        PooledObject po = platformPools[index].PopPool();
        return po as Platform;
    }
}
