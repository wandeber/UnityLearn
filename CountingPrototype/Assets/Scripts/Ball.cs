using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour {
  [SerializeField] private float baseSpeed = 5f;
  [SerializeField] public Rigidbody rb;
  [SerializeField] private ParticleSystem smoke;
  [SerializeField] private MeshRenderer meshRenderer;
  Vector3 startPosition;
  Quaternion startRotation;
  public bool isAvailable = true;
  public bool isFlying = false;
  private Vector3 spinDirection;
  private float startForceFactor;
  private float startForceFactorReduce = 0.05f;

  void Awake() {
    meshRenderer.enabled = false;
    startPosition = transform.position;
    startRotation = transform.rotation;
  }

  void Start() {
    Restart();
  }

  void Update() {
    if (isFlying && startForceFactor > 0f) {
      //rb.AddForce(spinDirection * rb.velocity.magnitude * 1f, ForceMode.Acceleration);
      rb.AddForce(spinDirection * 1.5f * Mathf.Pow(1f+startForceFactor, 10f) * Time.deltaTime, ForceMode.Acceleration);
      startForceFactor -= startForceFactorReduce * Time.deltaTime;
    }
  }

  public void Shot(float currentForceFactor, Vector3 targetPosition) {
    startForceFactor = currentForceFactor;
    isAvailable = false;
    //Debug.Log("shot");
    //Debug.Log(transform.forward);
    //Debug.Log(transform.up);
    //Debug.Log(Vector3.Cross(transform.forward, transform.up));
    /*var dir = transform.forward;
    dir.y = 0.4f;*/
    var dir = (targetPosition - transform.position).normalized;
    rb.AddForce(dir * currentForceFactor * baseSpeed, ForceMode.Impulse);
    //Debug.Log(rb.velocity.magnitude);
    isFlying = true;
    spinDirection = new Vector3(-targetPosition.x, 0, 0).normalized;
    //rb.AddTorque(Vector3.left * currentForceFactor * baseSpeed * 1000, ForceMode.Impulse);
  }

  public void Hide() {
    meshRenderer.enabled = false;
    Instantiate(smoke, transform.position, Quaternion.identity);
    //StartCoroutine(SetActiveAfterSeconds(0.3f, false));
  }

  IEnumerator SmokeAfterSeconds(float seconds) {
    yield return new WaitForSeconds(seconds);
    Instantiate(smoke, transform.position, Quaternion.identity);
  }

  IEnumerator SetActiveAfterSeconds(float seconds, bool active) {
    yield return new WaitForSeconds(seconds);
    meshRenderer.enabled = active;
  }

  IEnumerator SetAvailableAfterSeconds(float seconds, bool available) {
    yield return new WaitForSeconds(seconds);
    isAvailable = available;
  }

  public void Restart() {
    isFlying = false;
    transform.position = startPosition;
    transform.rotation = startRotation;
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    Instantiate(smoke, transform.position, Quaternion.identity);
    StartCoroutine(SetActiveAfterSeconds(0.3f, true));
    StartCoroutine(SetAvailableAfterSeconds(0.7f, true));
  }

  public void Death() {
    //Debug.Log("death");
    Destroy(gameObject);
  }

  public void OnCollisionEnter(Collision collision) {
    //if (collision.gameObject.tag == "Ground") {
      //Debug.Log("ground");
      isFlying = false;
      //StartCoroutine(SetActiveAfterSeconds(0.3f, false));
    //}
  }
}
