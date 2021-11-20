using UnityEngine;



public class RotateScript: MonoBehaviour {
  public float rotationSpeed = 50.0f;


  void Update() {
    float hInput = Input.GetAxis("Horizontal");
    transform.Rotate(Vector3.up, hInput * rotationSpeed * Time.deltaTime);
  }
}
