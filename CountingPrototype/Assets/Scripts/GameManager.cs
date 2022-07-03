using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
  public static GameManager instance;
  [SerializeField] private float forceSelectorSpeed = 1f;
  [SerializeField] private Slider hForceSlider;
  [SerializeField] private Ball ball;
  [SerializeField] private GameObject mouse;
  [SerializeField] private int maxTime;
  [SerializeField] public Text TimeText;
  [SerializeField] public GameObject GameOverPanel;
  public bool gameActive;
  
  private int remainingTime;
  private Vector3 mousePosition;

  public float maxforceFactor = 1f;
  public float forceTarget;
  public float forceSource;
  private Rigidbody rb;
  private float selectedForceFactor;
  private float currentForceFactor;
  private bool selectingForceFactor = false;
  private float timeStartedSelectingForceFactor;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    rb = GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start() {
    gameActive = true;
    remainingTime = maxTime;
    StartCoroutine(CountSeconds());
  }

  void Update() {
    if (gameActive && selectingForceFactor) {
      //Debug.Log("selectingForceFactor");
      currentForceFactor = Mathf.Lerp(forceSource, forceTarget, forceSelectorSpeed * Time.time - timeStartedSelectingForceFactor);
      if (currentForceFactor == forceTarget) {
        var tmp = forceSource;
        forceSource = forceTarget;
        forceTarget = tmp;
        timeStartedSelectingForceFactor = Time.time;
      }
      hForceSlider.value = currentForceFactor;
      //Debug.Log(currentForceFactor);
    }
  }

  void ToggleForceDirection() {
    selectingForceFactor = !selectingForceFactor;
  }

  // Update is called once per frame
  void FixedUpdate() {
    //Debug.Log("FixedUpdate");
    //Debug.Log(Input.GetKeyDown(KeyCode.Space));
    if (gameActive) {
      Vector3 tmp = Input.mousePosition;
      tmp.z = 11f;
      mousePosition = Camera.main.ScreenToWorldPoint(tmp);
      mousePosition.z = 11f;
      mouse.transform.position = mousePosition;
      //Debug.Log(mousePosition);

      if (ball.isAvailable && !selectingForceFactor && Input.GetMouseButton(0)) {
        //Debug.Log("Start selectingForceFactor");
        currentForceFactor = 0f;
        selectingForceFactor = true;
        forceSource = 0f;
        forceTarget = maxforceFactor;
        timeStartedSelectingForceFactor = Time.time;
      }
      if (selectingForceFactor && !Input.GetMouseButton(0)) {
        //Debug.Log("Stop selectingForceFactor");
        selectingForceFactor = false;
        ball.Shot(currentForceFactor, mousePosition);
        timeStartedSelectingForceFactor = Time.time;
        StartCoroutine(ResetBall());
        StartCoroutine(ResetForceSlider());
      }
    }
  }

  IEnumerator ResetBall() {
    yield return new WaitForSeconds(2f);
    if (gameActive) {
      ball.Hide();
    }

    yield return new WaitForSeconds(0.5f);
    if (gameActive) {
      ball.Restart();
    }
  }

  IEnumerator CountSeconds() {
    while (remainingTime > 0) {
      yield return new WaitForSeconds(1f);
      remainingTime--;
      TimeText.text = "Time: " + remainingTime;
    }
    
    if (remainingTime == 0) {
      //Debug.Log("Game Over");
      GameOver();
    }
  }

  void GameOver() {
    gameActive = false;
    GameOverPanel.SetActive(true);
  }

  IEnumerator ResetForceSlider() {
    while (hForceSlider.value > 0f) {
      hForceSlider.value = Mathf.Lerp(currentForceFactor, 0f, forceSelectorSpeed * Time.time - timeStartedSelectingForceFactor);
      yield return null;
    }
  }

  public void Restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
