using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float enemyInterval = 3.5f;

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }
    
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

    }
}
