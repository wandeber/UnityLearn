using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager: MonoBehaviour {
  public Brick BrickPrefab;
  public int LineCount = 6;
  public float BallSpeed = 2.0f;
  public Rigidbody Ball;

  public Text HighscoreText;
  public Text ScoreText;
  public GameObject GameOverText;

  private bool m_Started = false;
  private int m_Points;

  private bool m_GameOver = false;


  private void Awake() {
    UserData.ScoreInfo highscore = UserData.Instance.savedData.getHighscore();
    if (highscore != null) {
      HighscoreText.text = $"Best Score : {highscore.name} : {highscore.score}";
    }
    if (UserData.Instance.savedData.lineCount > 0) {
      LineCount = UserData.Instance.savedData.lineCount;
    }
  }

  // Start is called before the first frame update
  void Start() {
    const float step = 0.6f;
    int perLine = Mathf.FloorToInt(4.0f / step);

    int[] pointCountArray = new int[LineCount];
    for (int i = 0; i < LineCount; ++i) {
      pointCountArray[i] = (i + 1) * 2;
      for (int x = 0; x < perLine; ++x) {
        Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
        var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
        brick.PointValue = pointCountArray[i];
        brick.onDestroyed.AddListener(AddPoint);
      }
    }
  }

  private void Update() {
    if (!m_Started) {
      if (Input.GetKeyDown(KeyCode.Space)) {
        m_Started = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * BallSpeed, ForceMode.VelocityChange);
      }
    }
    else if (m_GameOver) {
      if (Input.GetKeyDown(KeyCode.Space)) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
  }

  void AddPoint(int point) {
    m_Points += point;
    ScoreText.text = $"Score : {m_Points}";
  }

  public void GoToMainMenu() {
    SceneManager.LoadScene(0);
  }

  public void GameOver() {
    var currScore = new UserData.ScoreInfo() { 
      name = UserData.Instance.savedData.name,
      score = m_Points
    };
    Debug.Log(currScore.score);
    var savedData = UserData.Instance.savedData;
    var ranking = savedData.ranking;
    bool changed = false;
    if (ranking.Count < 3) { // Max scores to store.
      ranking.Add(currScore);
      changed = true;
    }
    else if (currScore.score > ranking[ranking.Count - 1].score) {
      ranking[ranking.Count - 1] = currScore;
      changed = true;
    }

    for (int i = ranking.Count - 1; i > 0 && ranking[i].score > ranking[i - 1].score; i--) {
      Debug.Log(i+". "+ ranking[i].score);
      var tmp = ranking[i];
      ranking[i] = ranking[i - 1];
      ranking[i - 1] = tmp;
    }
    
    if (changed) {
      UserData.Instance.SaveData();
    }

    m_GameOver = true;
    GameOverText.SetActive(true);
  }
}
