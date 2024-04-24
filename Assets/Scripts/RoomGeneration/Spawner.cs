using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] List<Transform> spawnTemplates;
    [SerializeField] GameObject enemyPrefab;

    void Start() {
        int index = Random.Range(0, spawnTemplates.Count);
        SpawnEnemy(spawnTemplates[index]);
    }

    void Update() {
        
    }

    void SpawnEnemy(Transform template) {
        foreach (Transform childTransform in template) {
            Instantiate(enemyPrefab, childTransform.position, Quaternion.identity);
        }
    }
}
