using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target: MonoBehaviour {
  protected float maxMinX = 3.056f;
  protected float maxY = 1.85f;
  protected float minY = 0.6f;
  public int Points = 1;

  void Awake() {
    Move();
  }

  private void OnTriggerEnter(Collider other) {
    if (GameManager.Instance.gameActive) {
      GameManager.Instance.Score += Points;
      Move();
    }
  }

  public virtual void Move() {
    transform.position = new Vector3(
      Random.Range(-maxMinX, maxMinX),
      Random.Range(minY, maxY),
      transform.position.z
    );
  }
}
