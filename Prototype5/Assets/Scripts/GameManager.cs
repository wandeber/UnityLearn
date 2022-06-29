using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
  public static GameManager Instance;
  public TextMeshProUGUI txtScore;
  public TextMeshProUGUI txtLifes;
  public GameObject panelGameOver;
  public GameObject panelMenu;
  public int score;
  public int initialLifes = 3;
  private int lifes;
  public int level = 1;
  public bool isGameActive;

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }

  void Start() {
    panelMenu.SetActive(true);
  }

  public void restartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void startGame() {
    panelMenu.SetActive(false);
    isGameActive = true;
    score = 0;
    lifes = initialLifes;
    updateScore();
    updateLifes();
    panelGameOver.SetActive(false);
    level = 1;
    SpawnManager.Instance.Spawn();
  }

  public void gameOver() {
    panelGameOver.SetActive(true);
    isGameActive = false;
  }

  public void addPoints(int points) {
    if (!isGameActive) return;
    score += points;
    updateScore();
  }

  public void setDifficulty(int difficulty) {
    switch (difficulty) {
      case 1:
        SpawnManager.Instance.timeBetweenSpawns = 5.0f;
        SpawnManager.Instance.timeLevelFactor = 1.1f;
        SpawnManager.Instance.startPositionRange = 4;
        break;
      case 2:
        SpawnManager.Instance.timeBetweenSpawns = 3.0f;
        SpawnManager.Instance.timeLevelFactor = 1.2f;
        SpawnManager.Instance.startPositionRange = 6;
        break;
      case 3:
        SpawnManager.Instance.timeBetweenSpawns = 2.0f;
        SpawnManager.Instance.timeLevelFactor = 1.3f;
        SpawnManager.Instance.startPositionRange = 8;
        break;
    }
    startGame();
  }

  public void addLife(int lifePoints = 1) {
    if (!isGameActive) return;
    lifes += lifePoints;
    if (lifes <= 0) {
      lifes = 0;
      gameOver();
    }
    updateLifes();
  }

  void updateScore() {
    txtScore.text = "Score: "+ score;
  }
  void updateLifes() {
    txtLifes.text = "Lifes: "+ lifes;
  }
}
