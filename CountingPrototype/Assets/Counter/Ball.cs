using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball: MonoBehaviour {
  public float maxforceFactor = 1f;
  private Rigidbody rb;
  private float selectedForceFactor;
  private float currentForceFactor;
  private bool selectingForceFactor = false;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start() {
    
  }

  void Update() {
    if (selectingForceFactor) {
      currentForceFactor = Mathf.Lerp(currentForceFactor, maxforceFactor, 0.1f);
    }
  }

  // Update is called once per frame
  void FixedUpdate() {
    if (!selectingForceFactor && Input.GetKeyDown(KeyCode.Space)) {
      selectingForceFactor = true;
      currentForceFactor = 0f;
    }
    else if (selectingForceFactor && Input.GetKeyUp(KeyCode.Space)) {
      selectingForceFactor = false;
    }
  }
}
