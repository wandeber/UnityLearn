using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager: MonoBehaviour {
  public static SpawnManager Instance;
  public List<GameObject> items;
  public float timeBetweenSpawns = 5.0f;
  public float timeLevelFactor = 1.1f;
  public int startPositionRange = 4;

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }

  IEnumerator NextSpawn() {
    yield return new WaitForSeconds(
      timeBetweenSpawns * GameManager.Instance.level * timeLevelFactor
    );
    if (GameManager.Instance.isGameActive) {
      var index = Random.Range(0, items.Count);
      Instantiate(items[index], RandomPosition(), items[index].transform.rotation);
      Spawn();
    }
  }

  public void Spawn() {
    StartCoroutine(NextSpawn());
  }

  Vector3 RandomPosition() {
    return new Vector3(Random.Range(-startPositionRange, startPositionRange), -6);
  }
}
