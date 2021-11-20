using UnityEngine;



public class PlayerController: MonoBehaviour {
  Rigidbody rb;
  private GameObject focalPoint;
  public float speed = 5.0f;


  void Awake() {
    rb = GetComponent<Rigidbody>();
    focalPoint = GameObject.Find("FocalPoint");
  }

  void Update() {
    float forwardInput = Input.GetAxis("Vertical");
    rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
  }
}
