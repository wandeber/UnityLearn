using System.Collections;
using UnityEngine;



public class SpawnManager: MonoBehaviour {
  public static SpawnManager instance;
  
  public GameObject enemyPrefab;
  public GameObject powerupPrefab;
  
  public float spawnRange = 9.0f;
  public int enemiesToSpawn = 3;
  public int enemiesAlive = 0;


  void Awake() {
    if (instance != null) {
      return;
    }
    instance = this;
  }

  void Start() {
    StartNewWave();
  }


  private void StartNewWave() {
    StartCoroutine(SpawnWave());
  }

  private IEnumerator SpawnWave() {
    SpawnPowerup();
    yield return new WaitForSeconds(3.0f);
    for (int i = 0; i < enemiesToSpawn; i++) {
      SpawnEnemy();
    }
    enemiesToSpawn++;
  }

  private void SpawnEnemy() {
    Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    enemiesAlive++;
  }
  private void SpawnPowerup() {
    Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
  }

  private Vector3 GenerateSpawnPosition() {
    return new Vector3(
      Random.Range(-spawnRange, spawnRange),
      0,
      Random.Range(-spawnRange, spawnRange)
    );
  }


  public void DestroyEnemy(GameObject go) {
    Destroy(go);
    if (--enemiesAlive <= 0) {
      StartNewWave();
    }
    Debug.Log(enemiesAlive);
  }
}
