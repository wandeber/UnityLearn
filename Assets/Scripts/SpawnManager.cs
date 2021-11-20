using UnityEngine;



public class SpawnManager: MonoBehaviour {
  public GameObject enemyPrefab;
  public float spawnRange = 9.0f;


  void Start() {
    Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
  }

  private Vector3 GenerateSpawnPosition() {
    return new Vector3(
      Random.Range(-spawnRange, spawnRange),
      0,
      Random.Range(-spawnRange, spawnRange)
    );
  }
}
