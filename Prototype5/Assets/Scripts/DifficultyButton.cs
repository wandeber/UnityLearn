using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton: MonoBehaviour {
  private Button button;
  public int difficulty = 1;

  // Start is called before the first frame update
  void Start() {
    button = GetComponent<Button>();
    button.onClick.AddListener(setDifficulty);
  }

  // Update is called once per frame
  void Update() {
    
  }

  public void setDifficulty() {
    GameManager.Instance.setDifficulty(difficulty);
  }
}
