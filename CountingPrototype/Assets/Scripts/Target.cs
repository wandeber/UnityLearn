using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target: MonoBehaviour {
  public Text CounterText;
  private int Count = 0;
  private float maxMinX = 3.056f;
  private float maxY = 1.85f;
  private float minY = 0.6f;

  void Awake() {
    Count = 0;
    UpdateText();
    Move();
  }

  private void OnTriggerEnter(Collider other) {
    if (GameManager.instance.gameActive) {
      Count += 1;
      UpdateText();
      Move();
    }
  }

  void UpdateText() {
    CounterText.text = "Score: " + Count;
  }

  public void Move() {
    transform.position = new Vector3(
      Random.Range(-maxMinX, maxMinX),
      Random.Range(minY, maxY),
      transform.position.z
    );
  }
}
