using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int maxEnemies = 10;
    [System.NonSerialized] public int maxBosses = 1;
    [System.NonSerialized] public int bossCount = 0;
    public int enemyCount = 0;
    public float spawnChance = 30f; // chance to spawn enemy per second

    public GameObject bossPrefab;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
}
