using System.Collections;
using UnityEngine;



public class PlayerController: MonoBehaviour {
  Rigidbody rb;
  private GameObject focalPoint;
  private float forwardInput = 0.0f;


  public GameObject powerupIndicator;
  public float speed = 5.0f;
  public bool _hasPowerup = false;
  public bool hasPowerup {
    get => _hasPowerup;
    set {
      _hasPowerup = value;
      powerupIndicator.SetActive(value);
    }
  }
  public float powerUpStrength = 15.0f;



  void Awake() {
    rb = GetComponent<Rigidbody>();
    focalPoint = GameObject.Find("FocalPoint");
  }

  void Update() {
    forwardInput = Input.GetAxis("Vertical");
  }

  void FixedUpdate() {
    rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    powerupIndicator.transform.position = transform.position;
  }

  public void OnTriggerEnter(Collider other) {
    Debug.Log("Collision");
    if (other.CompareTag("Powerup")) {
      Destroy(other.gameObject);
      hasPowerup = true;
      StartCoroutine(Powerup());
    }
  }

  public void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {
      Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
      if (otherRb == null) {
        return;
      }

      var dir = (collision.gameObject.transform.position - transform.position).normalized;
      otherRb.AddForce(dir * powerUpStrength, ForceMode.Impulse);
    }
  }

  private IEnumerator Powerup() {
    hasPowerup = true;
    yield return new WaitForSeconds(7.0f);
    Debug.Log("asdasd");
    hasPowerup = false;
    
  }
}
