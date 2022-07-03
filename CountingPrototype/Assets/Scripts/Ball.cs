using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour {
  [SerializeField] private float baseSpeed = 5f;
  [SerializeField] public Rigidbody rb;
  Vector3 startPosition;
  Quaternion startRotation;
  public bool isAvailable = true;

  void Awake() {
    startPosition = transform.position;
    startRotation = transform.rotation;
  }

  public void Shot(float currentForceFactor) {
    isAvailable = false;
    Debug.Log("shot");
    Debug.Log(transform.forward);
    Debug.Log(transform.up);
    Debug.Log(Vector3.Cross(-transform.forward, transform.up));
    var dir = -transform.forward;
    dir.y = 0.4f;
    rb.AddForce(dir * currentForceFactor * baseSpeed, ForceMode.Impulse);
  }

   public void Restart() {
    transform.position = startPosition;
    transform.rotation = startRotation;
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    isAvailable = true;
  }

  public void Death() {
    Debug.Log("death");
    Destroy(gameObject);
  }
}
