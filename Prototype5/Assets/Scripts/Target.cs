using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target: MonoBehaviour {
  public float minStartForce = 12.0f;
  public float maxStartForce = 16.0f;
  public float forceFactor = 1.0f;
  public int eatPoints = 5;
  public int eatLifePoints = 0;
  public int fallLifePoints = -1;
  private Rigidbody rb;
  public float torqueRange = 10;
  public GameObject fxDeath;


  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void Start() {
    rb.AddForce(RandomForce(), ForceMode.Impulse);
    rb.AddTorque(RandomTorque(), ForceMode.Impulse);
  }

  Vector3 RandomForce() {
    return Vector3.up * Random.Range(minStartForce, maxStartForce);
  }

  Vector3 RandomTorque() {
    return new Vector3(
      Random.Range(-torqueRange, torqueRange),
      Random.Range(-torqueRange, torqueRange),
      Random.Range(-torqueRange, torqueRange)
    );
  }

  private void OnMouseDown() {
    if (eatPoints != 0) {
      GameManager.Instance.addPoints(eatPoints);
    }
    if (eatLifePoints != 0) {
      GameManager.Instance.addLife(eatLifePoints);
    }

    if (fxDeath != null) {
      Instantiate(fxDeath, transform.position, transform.rotation);
    }
    Destroy(gameObject);
  }
}
