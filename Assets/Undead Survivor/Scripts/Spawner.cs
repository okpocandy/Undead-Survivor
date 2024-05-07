using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        if (timer > spawnData[level].spawnPeriod)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnPeriod;
    public int spriteType;
    public int monsterHp;
    public float monsterSpeed;
}