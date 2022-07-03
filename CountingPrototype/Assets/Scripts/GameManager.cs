using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour {
  [SerializeField] private float forceSelectorSpeed = 1f;
  [SerializeField] private Slider hForceSlider;
  [SerializeField] private Ball ball;

  public float maxforceFactor = 1f;
  public float forceTarget;
  public float forceSource;
  private Rigidbody rb;
  private float selectedForceFactor;
  private float currentForceFactor;
  private bool selectingForceFactor = false;
  private float timeStartedSelectingForceFactor;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start() {
    
  }

  void Update() {
    if (selectingForceFactor) {
      //Debug.Log("selectingForceFactor");
      currentForceFactor = Mathf.Lerp(forceSource, forceTarget, forceSelectorSpeed * Time.time - timeStartedSelectingForceFactor);
      if (currentForceFactor == forceTarget) {
        var tmp = forceSource;
        forceSource = forceTarget;
        forceTarget = tmp;
        timeStartedSelectingForceFactor = Time.time;
      }
      hForceSlider.value = currentForceFactor;
      Debug.Log(currentForceFactor);
    }
  }

  void ToggleForceDirection() {
    selectingForceFactor = !selectingForceFactor;
  }

  // Update is called once per frame
  void FixedUpdate() {
    //Debug.Log("FixedUpdate");
    //Debug.Log(Input.GetKeyDown(KeyCode.Space));
    if (ball.isAvailable && !selectingForceFactor && Input.GetKey(KeyCode.Space)) {
      Debug.Log("Start selectingForceFactor");
      currentForceFactor = 0f;
      selectingForceFactor = true;
      forceSource = 0f;
      forceTarget = maxforceFactor;
      timeStartedSelectingForceFactor = Time.time;
    }
    if (selectingForceFactor && !Input.GetKey(KeyCode.Space)) {
      Debug.Log("Stop selectingForceFactor");
      selectingForceFactor = false;
      ball.Shot(currentForceFactor);
      timeStartedSelectingForceFactor = Time.time;
      StartCoroutine(ResetBall());
      StartCoroutine(ResetForceSlider());
    }
  }

  IEnumerator ResetBall() {
    yield return new WaitForSeconds(5f);
    ball.Restart();
  }

  IEnumerator ResetForceSlider() {
    while (hForceSlider.value > 0f) {
      hForceSlider.value = Mathf.Lerp(currentForceFactor, 0f, forceSelectorSpeed * Time.time - timeStartedSelectingForceFactor);
      yield return null;
    }
  }
}
