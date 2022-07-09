using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MoveableTarget: Target {
  private float speed = 1.0f;
  private Vector2 direction;


  // Update is called once per frame
  void Update() {
    if (transform.position.x < -maxMinX) {
      direction = new Vector2(
        Random.Range(0.0f, 1.0f),
        Random.Range(-1.0f, 1.0f)
      ).normalized;
    }
    else if (maxMinX < transform.position.x) {
      direction = new Vector2(
        Random.Range(-1.0f, 0.0f),
        Random.Range(-1.0f, 1.0f)
      ).normalized;
    }
    else if (transform.position.y < minY) {
      direction = new Vector2(
        Random.Range(-1.0f, 1.0f),
        Random.Range(0.0f, 1.0f)
      ).normalized;
    }
    else if (maxY < transform.position.y) {
      direction = new Vector2(
        Random.Range(-1.0f, 1.0f),
        Random.Range(-1.0f, 0.0f)
      ).normalized;
    }

    transform.Translate(direction * speed * Time.deltaTime);
  }

  // POLYMORPHISM
  public override void Move() {
    base.Move();
    RandomDirection();
  }

  public void RandomDirection() {
    direction = new Vector2(
      Random.Range(-1.0f, 1.0f),
      Random.Range(-1.0f, 1.0f)
    ).normalized;
  }
}
