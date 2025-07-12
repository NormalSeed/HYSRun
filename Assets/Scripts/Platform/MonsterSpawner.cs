using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;

    [SerializeField] List<GameObject> enimies = new List<GameObject>();

    public void SpawnEnemy()
    {
        int index = Random.Range(0, enimies.Count);

        Instantiate(enimies[index], spawnPoint.transform.position, Quaternion.identity);
    }
}
